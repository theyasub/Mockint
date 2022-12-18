using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserAccountsController : RESTFulController
{
    public IAccountService accountService { get; set; }
    public UserAccountsController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    /// <summary>
    /// Log in through parametrs
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> LogInAsync([FromForm] UserForLoginDto viewModel)
        => Ok(new { Token = await accountService.UserLogInAsync(viewModel) });

    /// <summary>
    /// Verify exist email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>

    [HttpPost("verifyemail")]
    public async Task<IActionResult> VerifyEmail([FromBody] EmailVerify email)
        => Ok(await accountService.VerifyEmailAsync(email));

    /// <summary>
    /// Send notification to email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>

    [HttpPost("sendcode"), AllowAnonymous]
    public async Task<IActionResult> SendToEmail([FromBody] SentToEmail email)
    {
        await accountService.SendCodeAsync(email);
        return Ok();
    }

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="forgetPassword"></param>
    /// <returns></returns>
    [HttpPost("reset-password"), AllowAnonymous]
    public async Task<IActionResult> ForgotPasswordAsync([FromQuery] UserForResertPasswordDto forgetPassword)
        => Ok(await accountService.VerifyPasswordAsync(forgetPassword));

}