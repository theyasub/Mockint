using System.Linq.Expressions;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.Interfaces;

public interface IUserDetailsService
{
    ValueTask<UserDetail> CreateAsync(UserDetailsForCreationDto userDetailsForCreationDto);
    
    ValueTask<UserDetail> GetAsync(Expression<Func<UserDetail, bool>> expression);
    
    ValueTask<IEnumerable<UserDetail>> GetAllAsync(PaginationParams @params, Expression<Func<UserDetail, bool>> expression = null);
    
    ValueTask<UserDetail> UpdateAsync(long id, UserDetailsForCreationDto userDetailsForCreationDto);
    
    ValueTask<bool> DeleteAsync(Expression<Func<UserDetail, bool>> expression);
}