using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.Interfaces;

public interface IAccountService
{
    /// <summary>
    /// UserLoginAsync is a service that handles the process of logging a user into the system.
    /// It accepts a UserForLoginDto object as input, which contains the user's Gmail address and password.
    /// </summary>
    /// <param name="userForLoginDto"></param>
    /// <returns></returns>
    ValueTask<string> UserLogInAsync(UserForLoginDto userForLoginDto);

    /// <summary>
    /// VerifyEmailAsync is a service that handles the process of verifying the email address of a user.
    /// It accepts an EmailVerify object as input, which contains the user's Gmail address and a code that was sent to their email.
    /// </summary>
    /// <param name="emailVerify"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    ValueTask<bool> VerifyEmailAsync(EmailVerify emailVerify);

    /// <summary>
    /// SendCodeAsync is a service that handles the process of sending a verification code to a user's email address
    /// It accepts a SentToEmail object as input, which contains the user's email address
    /// </summary>
    /// <param name="sentToEmail"></param>
    ValueTask SendCodeAsync(SentToEmail sentToEmail);

    /// <summary>
    /// VerifyPasswordAsync is a service that handles the process of resetting a user's password.
    /// It accepts a UserForResetPasswordDto object as input, which contains the user's email address and the new password they want to set
    /// </summary>
    /// <param name="userForResertPasswordDto"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    ValueTask<bool> VerifyPasswordAsync(UserForResertPasswordDto userForResertPasswordDto);
}