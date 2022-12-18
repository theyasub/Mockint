using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Chats;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Message> CreateAsync(MessageCreationDto messageCreationDto)
    {
        User existUser = await unitOfWork.Users.GetAsync(u => u.Id == messageCreationDto.UserId);

        if (existUser == null)
            throw new CustomException(404, "User is not found.");


        Room existRoom = await unitOfWork.Rooms.GetAsync(r => r.Id == messageCreationDto.RoomId);

        if (existRoom == null)
            throw new CustomException(404, "Chat is not found.");

        Message message = messageCreationDto.Adapt<Message>();
        message.Create();

        message = await unitOfWork.Messages.CreateAsync(message);
        await unitOfWork.SaveChangesAsync();

        return message;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Message, bool>> expression)
    {
        Message existUser = await unitOfWork.Messages.GetAsync(expression);

        if (existUser == null)
            throw new CustomException(404, "Message is not found.");

        await unitOfWork.Messages.DeleteAsync(expression);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Message>> GetAllAsync(PaginationParams @params, Expression<Func<Message, bool>> expression = null)
    {
        return await unitOfWork.Messages
            .GetAll(expression)
            .ToPagedList(@params)
            .ToListAsync();
    }

    public async ValueTask<Message> GetAsync(Expression<Func<Message, bool>> expression)
    {
        Message existMessage = await unitOfWork.Messages.GetAsync(expression);

        if (existMessage == null)
            throw new CustomException(404, "Message not Found");

        return await unitOfWork.Messages.GetAsync(expression);
    }
    public async ValueTask<Message> UpdateAsync(int id, string messageCreationDto)
    {
        Message existMessage = await unitOfWork.Messages.GetAsync(m => m.Id == id);

        if (existMessage == null)
            throw new CustomException(404, "Message not found");

        existMessage.Update();

        existMessage.Content = messageCreationDto;
        existMessage = await unitOfWork.Messages.UpdateAsync(existMessage);
        await unitOfWork.SaveChangesAsync();

        return existMessage;
    }
}
