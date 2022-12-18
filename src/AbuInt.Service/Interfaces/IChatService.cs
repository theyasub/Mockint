using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using System.Linq.Expressions;

namespace AbuInt.Service.Interfaces;

public interface IChatService
{
    /// <summary>
    /// Create Private chat
    /// </summary>
    /// <returns></returns>
    ValueTask<Room> CreatePrivateChatAsync(int chatUserId);

    /// <summary>
    /// Delete Private chat
    /// </summary>
    /// <param name="privateChatId"></param>
    /// <returns></returns>
    ValueTask<bool> DeletePrivateChatAsync(int privateChatId);

    /// <summary>
    /// Get private chat Data
    /// </summary>
    /// <param name="privateChatId"></param>
    /// <returns></returns>
    ValueTask<Room> GetPrivateChatAsync(int privateChatId);

    /// <summary>
    /// get all private chats by given expression
    /// </summary>
    /// <param name="params"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<Room>> GetAllAsync(PaginationParams @params, Expression<Func<Room, bool>> expression = null);
}


