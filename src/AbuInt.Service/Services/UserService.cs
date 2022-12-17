using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces.Users;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class UserService : IUserService
{
    public IUnitOfWork unitOfWork { get; set; }
    public UserService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public ValueTask<User> CreateAsync(UserForCreationDto userForCreationDto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> DeleteAsync(Expression<Func<UserForCreationDto, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<UserForCreationDto, bool>> expression = null, string search = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> GetAsync(Expression<Func<UserForCreationDto, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> GetInfoAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<User> UpdateAsync(int id, UserForCreationDto userForCreationDto)
    {
        throw new NotImplementedException();
    }
}
