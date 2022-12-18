using AbuInt.Domain.Entities.Chats;
using AbuInt.Service.DTOs.Chats;

namespace AbuInt.Service.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Create Private 
    /// </summary>
    /// <returns></returns>
    ValueTask<Room> CreatePrivateChatAsync(int chatUserId);

}


