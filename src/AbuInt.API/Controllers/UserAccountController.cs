using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccountController : RESTFulController
{
    public IAccountService accountService { get; set; }
    public UserAccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }
    
    /// <summary>
    ///  Login user
    /// </summary>
    /// <param name="userForLoginDto"></param>
    /// <returns></returns>
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LogInAsync([FromForm] UserForLoginDto viewModel)
        => Ok(new { Token = await accountService.UserLoginAsync(viewModel) });

    [HttpPost("verifyemail")]
    public async Task<IActionResult> VerifyEmail([FromBody] EmailVerify email)
        => Ok(await accountService.VerifyEmailAsync(email));

    [HttpPost("sendcode"), AllowAnonymous]
    public async Task<IActionResult> SendToEmail([FromBody] SentToEmail email)
    {
        await accountService.SendCodeAsync(email);
        return Ok();
    }

    [HttpPost("reset-password"), AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync([FromQuery] UserForResertPasswordDto forgetPassword)
        => Ok(await accountService.VerifyPasswordAsync(forgetPassword));

}