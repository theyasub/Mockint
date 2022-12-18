using AbuInt.Domain.Entities.Chats;

namespace AbuInt.Service.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Create Private 
    /// </summary>
    /// <returns></returns>
    ValueTask<Room> CreatePrivateChatAsync(int chatUserId);

}


