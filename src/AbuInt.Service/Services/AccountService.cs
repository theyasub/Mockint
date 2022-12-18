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
    private readonly IMemoryCache memoryCache;
    private readonly EmailHelper emailHelper;
    public AccountService(IUnitOfWork unitOfWork, FIleHelper fileHelper, IMemoryCache memoryCache, EmailHelper emailHelper, IAuthService authService)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelper = fileHelper;
        this.memoryCache = memoryCache;
        this.emailHelper = emailHelper;
        this.authService = authService;
    }

    /// <summary>
    /// UserLoginAsync is a service that handles the process of logging a user into the system.
    /// It accepts a UserForLoginDto object as input, which contains the user's Gmail address and password.
    /// </summary>
    /// <param name="userForLoginDto"></param>
    /// <returns></returns>
    public async ValueTask<string> UserLoginAsync(UserForLoginDto userForLoginDto) => 
        await authService.GenerateToken(userForLoginDto.Gmail, userForLoginDto.Password);

    /// <summary>
    /// VerifyEmailAsync is a service that handles the process of verifying the email address of a user.
    /// It accepts an EmailVerify object as input, which contains the user's Gmail address and a code that was sent to their email.
    /// </summary>
    /// <param name="emailVerify"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async ValueTask<bool> VerifyEmailAsync(EmailVerify emailVerify)
    {
        var entity = await unitOfWork.Users.GetAsync(user => user.Gmail == emailVerify.Gmail);

        if (entity is null)
            throw new CustomException(404, "User not found");

        if (memoryCache.TryGetValue(emailVerify.Gmail, out int exceptedCode))
        {
            if (exceptedCode != emailVerify.Code)
                throw new CustomException(400, "Code is wrong");

            entity.IsEmailVerified = true;

            await unitOfWork.Users.UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();

            return true;
        }
        
        throw new CustomException(400, "Code is expired");
        

    }

    /// <summary>
    /// SendCodeAsync is a service that handles the process of sending a verification code to a user's email address
    /// . It accepts a SentToEmail object as input, which contains the user's email address
    /// </summary>
    /// <param name="sentToEmail"></param>
    public async ValueTask SendCodeAsync(SentToEmail sentToEmail)
    {
        int code = new Random().Next(1000, 9999);

        memoryCache.Set(sentToEmail.Email, code, TimeSpan.FromMinutes(10));

        var message = new EmailMessage()
        {
            To = sentToEmail.Email,
            Subject = "Verification code",
            Body = code
        };
        await emailHelper.SendAsync(message);
    }

    /// <summary>
    /// VerifyPasswordAsync is a service that handles the process of resetting a user's password.
    /// It accepts a UserForResetPasswordDto object as input, which contains the user's email address and the new password they want to set
    /// </summary>
    /// <param name="userForResertPasswordDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async ValueTask<bool> VerifyPasswordAsync(UserForResertPasswordDto userForResertPasswordDto)
    {
        var user = await unitOfWork.Users.GetAsync(p => p.Gmail == userForResertPasswordDto.Email);

        if (user is null)
            throw new CustomException(404, "user not found!");

        if (user.IsEmailVerified is false)
            throw new CustomException(400, "Gmail did not verified!");

        var changedPassword = SecurityService.Encrypt(userForResertPasswordDto.Password, user.Salt);

        user.Password = changedPassword;

        await unitOfWork.Users.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        throw new CustomException(200, "true");
    }
}