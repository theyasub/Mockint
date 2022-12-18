using System.Reflection.Metadata.Ecma335;
using AbuInt.Data.IRepositories;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Helpers;
using AbuInt.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AbuInt.Service.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IAuthService authService;
    private readonly FIleHelper fileHelper;
    private readonly ICacheService cacheService;
    private readonly EmailHelper emailHelper;
    public AccountService(
        IUnitOfWork unitOfWork,
        FIleHelper fileHelper,
        IMemoryCache memoryCache,
        EmailHelper emailHelper,
        IAuthService authService)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelper = fileHelper;
        this.cacheService = cacheService;
        this.emailHelper = emailHelper;
        this.authService = authService;
    }

    public async ValueTask<string> UserLogInAsync(UserForLoginDto userForLoginDto) => 
        await authService.GenerateToken(userForLoginDto.Gmail, userForLoginDto.Password);

    public async ValueTask<bool> VerifyEmailAsync(EmailVerify emailVerify)
    {
        var entity = await unitOfWork.Users.GetAsync(user => user.Gmail == emailVerify.Gmail);

        if (entity is null)
            throw new CustomException(404, "User not found");

        if (cacheService.GetData<int>(emailVerify.Gmail) > 999)
        {
            if (cacheService.GetData<int>(emailVerify.Gmail) != emailVerify.Code)
                throw new CustomException(400, "Code is wrong");

            entity.IsEmailVerified = true;

            await unitOfWork.Users.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();

            return true;
        }
        throw new CustomException(400, "Code is expired");
    }

    public async ValueTask SendCodeAsync(SentToEmail sentToEmail)
    {
        int code = new Random().Next(1000, 9999);

        cacheService.SetData(sentToEmail.Email, code, TimeSpan.FromMinutes(10));


        var message = new EmailMessage()
        {
            To = sentToEmail.Email,
            Subject = "Verification code",
            Body = code
        };
        await emailHelper.SendAsync(message);
    }

    public async ValueTask<bool> VerifyPasswordAsync(UserForResertPasswordDto userForResertPasswordDto)
    {
        var user = await unitOfWork.Users.GetAsync(p => p.Gmail == userForResertPasswordDto.Email);

        if (user is null)
            throw new CustomException(404, "user not found!");

        if (user.IsEmailVerified is false)
            throw new CustomException(400, "Gmail did not verified!");

        var changedPassword = SecurityService.Encrypt(userForResertPasswordDto.Password, user.Salt.ToString());

        user.Password = changedPassword;

        await unitOfWork.Users.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        throw new CustomException(200, "true");
    }
}