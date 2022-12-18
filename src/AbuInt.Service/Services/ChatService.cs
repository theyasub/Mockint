using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class ChatService : IChatService
{
    private readonly IUnitOfWork unitOfWork;

    public ChatService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Room> CreatePrivateChatAsync(int chatUserId)
    {
        User existUser = await unitOfWork.Users.GetAsync(x => x.Id == chatUserId);

        if (existUser is null)
            throw new CustomException(404, "This user is not found.");

        if (HttpContextHelper.UserId == chatUserId)
            throw new CustomException(400, "Both of users are same.");

        Room room = new Room();
        room.RoomType = Domain.Enums.RoomType.Private;

        room = await unitOfWork.Rooms.CreateAsync(room);
        await unitOfWork.SaveChangesAsync();

        Participant participant = new Participant();
        participant.UserId = HttpContextHelper.UserId ?? -1;
        participant.RoomId = room.Id;
        participant.Create();

        await unitOfWork.Participants.CreateAsync(participant);

        participant.UserId = chatUserId;
        participant.Create();

        await unitOfWork.Participants.CreateAsync(participant);

        await unitOfWork.SaveChangesAsync();

        return room;

    }

    public async ValueTask<bool> DeletePrivateChatAsync(int privateChatId)
    {
        Room existPrivateChat = await unitOfWork.Rooms.GetAsync(r => r.Id == privateChatId);

        if (existPrivateChat is null)
            throw new CustomException(404, "Chat not found");

        foreach (var part in unitOfWork.Participants.GetAll(p => p.RoomId == privateChatId))
            await unitOfWork.Participants.DeleteAsync(x => x.Id == part.Id);

        await unitOfWork.Rooms.DeleteAsync(r => r.Id == privateChatId);

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Room>> GetAllAsync(PaginationParams @params, Expression<Func<Room, bool>> expression = null)
    {
        IQueryable<Room> privateChats = unitOfWork.Rooms.GetAll(expression, new string[]
        {
            "Participants", "Messages"
        });

        return await privateChats.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<Room> GetPrivateChatAsync(int privateChatId)
    {
        Room existPrivateCHat = await unitOfWork.Rooms.GetAsync(x => x.Id == privateChatId, new string[]
        {
            "Participants", "Messages"
        });

        if (existPrivateCHat is null)
            throw new CustomException(404, "Privaet chat not fount");

        return existPrivateCHat;
    }
}
