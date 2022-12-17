using System.Linq.Expressions;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.Interfaces.Users;

public interface IUserService
{
    /// <summary>
    /// Create User 
    /// </summary>
    /// <param name="userForCreationDto"></param>
    /// <returns></returns>
    ValueTask<UserForCreationDto> CreateAsync(UserForCreationDto userForCreationDto);
    
    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userForCreationDto"></param>
    /// <returns></returns>
    ValueTask<UserForCreationDto> UpdateAsync(int id, UserForCreationDto userForCreationDto);
    
    /// <summary>
    ///  Get User's information
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<UserForCreationDto> GetAsync(Expression<Func<UserForCreationDto, bool>> expression);
    
    /// <summary>
    ///  Get All User's Informations
    /// </summary>
    /// <param name="params"></param>
    /// <param name="expression"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<UserForCreationDto>> GetAllAsync(PaginationParams @params, Expression<Func<UserForCreationDto, bool>> expression = null, string search = null);
    
    /// <summary>
    /// Delete User's Information
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(Expression<Func<UserForCreationDto, bool>> expression);
    
    /// <summary>
    /// Get Current User
    /// </summary>
    /// <returns></returns>
    ValueTask<UserForCreationDto> GetInfoAsync();
}