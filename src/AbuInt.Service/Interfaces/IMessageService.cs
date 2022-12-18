using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Service.DTOs.Chats;
using System.Linq.Expressions;

namespace AbuInt.Service.Interfaces;

public interface IMessageService
{
    /// <summary>
    /// Creaing new Message 
    /// </summary>
    /// <param name="MessageCreationDto"></param>
    /// <returns></returns>
    public ValueTask<Message> CreateAsync(MessageCreationDto MessageCreationDto);

    /// <summary>
    /// Get one Message info by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public ValueTask<Message> GetAsync(Expression<Func<Message, bool>> expression);

    /// <summary>
    /// Get all Message information with pagenation and can validate data with expresstion and search text
    /// </summary>
    /// <param name="params"></param>
    /// <param name="expression"></param>
    /// <param name="searchText"></param>
    /// <returns></returns>
    public ValueTask<IEnumerable<Message>> GetAllAsync(PaginationParams @params, Expression<Func<Message, bool>> expression = null);

    /// <summary>
    /// Update Message
    /// </summary>
    /// <param name="MessageCreationDto"></param>
    /// <returns></returns>
    public ValueTask<Message> UpdateAsync(int id, string messageCreationDto);

    /// <summary>
    /// Delete Message by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public ValueTask<bool> DeleteAsync(Expression<Func<Message, bool>> expression);
}
