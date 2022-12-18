using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class UserService : IUserService
{
    public IUnitOfWork unitOfWork { get; set; }
    public UserService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<User> CreateAsync(UserForCreationDto userForCreationDto)
    {
        User user = await this.unitOfWork.Users.GetAsync(user =>
            user.Username.Equals(userForCreationDto.Username));

        if (user is not null)
            throw new CustomException(400, "User alredy exists");

        user = userForCreationDto.Adapt<User>();

        var result = await this.unitOfWork.Users.CreateAsync(user);
        user.Create();

        await this.unitOfWork.SaveChangesAsync();

        return result;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        User user = await this.unitOfWork.Users.GetAsync(expression);

        if (user is null)
            throw new CustomException(404, "User not found");

        await this.unitOfWork.Users.DeleteAsync(expression);
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync(
        PaginationParams @params, 
        Expression<Func<User, bool>> expression = null,
        string search = null)
    {
        var users = this.unitOfWork.Users.GetAll(expression, isTracking: false);

        if (!string.IsNullOrEmpty(search))
            await users.Where(user => user.FirstName.Contains(search) ||
                       user.LastName.Contains(search))
                       .ToPagedList(@params).ToListAsync();
        
        return await users.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
    {
        User user = await this.unitOfWork.Users.GetAsync(expression);

        if (user is null)
            throw new CustomException(404, "User not found");

        return user;
    }

    public async ValueTask<User> GetInfoAsync()
        => await this.unitOfWork.Users.GetAsync(user => user.Id.Equals(HttpContextHelper.UserId));

    public async ValueTask<User> UpdateAsync(int id, UserForCreationDto userForCreationDto)
    {
        User user = await this.unitOfWork.Users.GetAsync(user => user.Id.Equals(id));    

        if (user is null)
            throw new CustomException(404, "User not found");

        var mappedUser = userForCreationDto.Adapt(user);

        user = await this.unitOfWork.Users.UpdateAsync(mappedUser);
        await this.unitOfWork.SaveChangesAsync();

        return user;
    }
}
