using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Helpers;
using AbuInt.Service.Interfaces.Users;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly FIleHelper fileHelper;
    public UserService(IUnitOfWork unitOfWork, FIleHelper fileHelper)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelper = fileHelper;
    }

    public async ValueTask<User> CreateAsync(UserForCreationDto userForCreationDto)
    {
        User user = await this.unitOfWork.Users.GetAsync(user =>
            user.Username.Equals(userForCreationDto.Username));

        if (user is not null)
            throw new CustomException(400, "User alredy exists");

        user = userForCreationDto.Adapt<User>();
        user.Password = SecurityService.Encrypt(userForCreationDto.Password, user.Salt.ToString());
        if (userForCreationDto.Image is not null)
        {
            var attachmentData = await fileHelper.SaveAsync(userForCreationDto.Image);

            Asset newAsset = new Asset()
            {
                Name = attachmentData.fileName,
                Path = attachmentData.filePath
            };

            newAsset.Create();

            newAsset = await unitOfWork.Assets.CreateAsync(newAsset);
            await unitOfWork.SaveChangesAsync();

            user.ImageId = newAsset.Id;
        }

        user.Create();
        var result = await this.unitOfWork.Users.CreateAsync(user);
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
            await users.Where(user => user.FirstName == search ||
                       user.LastName == search)
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
