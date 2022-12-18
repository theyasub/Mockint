using AbuInt.Service.DTOs.Users;

namespace AbuInt.Service.Interfaces;

public interface IAccountService
{
    ValueTask<string?> UserLoginAsync(UserForLoginDto userForLoginDto);
    ValueTask<bool> VerifyEmailAsync(EmailVerify emailVerify);
    ValueTask SendCodeAsync(SentToEmail sentToEmail);
    ValueTask<bool> VerifyPasswordAsync(UserForResertPasswordDto userForResertPasswordDto);
}