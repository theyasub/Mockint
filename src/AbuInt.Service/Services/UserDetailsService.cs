using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class UserDetailsService : IUserDetailsService
{
    private readonly IUnitOfWork unitOfWork;

    public UserDetailsService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<UserDetail> CreateAsync(UserDetailsForCreationDto userDetailsForCreationDto)
    {
        UserDetail userDetail = await this.unitOfWork.UserDetails.GetAsync(s =>
                s.Phonenumber.Equals(userDetailsForCreationDto.PhoneNumber));

        if (userDetail is not null)
            throw new CustomException(400, "This User Details already Exists");

        userDetail = userDetailsForCreationDto.Adapt<UserDetail>();

        userDetail.Create();
        var res = await this.unitOfWork.UserDetails.CreateAsync(userDetail);
        await unitOfWork.SaveChangesAsync();
        return res;
    }

    public async ValueTask<UserDetail> GetAsync(Expression<Func<UserDetail, bool>> expression)
    {
        var userDetail = await this.unitOfWork.UserDetails.GetAsync(expression);

        if (userDetail is null)
            throw new CustomException(404, "User details not found");

        return userDetail;
    }

    public async ValueTask<IEnumerable<UserDetail>> GetAllAsync(PaginationParams @params, Expression<Func<UserDetail, bool>> expression = null)
    {
        var userDetails = this.unitOfWork.UserDetails.GetAll(expression, isTracking: false);
        return await userDetails.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<UserDetail> UpdateAsync(long id, UserDetailsForCreationDto userDetailsForCreationDto)
    {
        var userDetail = await this.unitOfWork.UserDetails.GetAsync(us => us.Id.Equals(id));

        if (userDetail is null)
            throw new CustomException(404, "User details not found");

        var mappedUs = userDetailsForCreationDto.Adapt(userDetail);

        userDetail = await this.unitOfWork.UserDetails.UpdateAsync(mappedUs);
        await this.unitOfWork.SaveChangesAsync();

        return userDetail;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<UserDetail, bool>> expression)
    {
        var userDetail = await this.unitOfWork.UserDetails.GetAsync(expression);

        if (userDetail is null)
            throw new CustomException(404, "User Details not found");

        await this.unitOfWork.UserDetails.DeleteAsync(expression);
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }
}