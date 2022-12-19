using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserDetailsController : RESTFulController
{
    private readonly IUserDetailsService userDetailsService;

    public UserDetailsController(IUserDetailsService userDetailsService)
    {
        this.userDetailsService = userDetailsService;
    }

    /// <summary>
    /// Create user detailed data
    /// </summary>
    /// <param name="userDetailsForCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<ActionResult<UserDetail>> CreateAsync([FromForm] UserDetailsForCreationDto userDetailsForCreationDto)
        => Ok(await this.userDetailsService.CreateAsync(userDetailsForCreationDto));

    /// <summary>
    /// Update user detailed info
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userDetailsForCreationDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async ValueTask<ActionResult<UserDetail>> UpdateAsync(int id, [FromForm] UserDetailsForCreationDto userDetailsForCreationDto)
        => Ok(await this.userDetailsService.UpdateAsync(id, userDetailsForCreationDto));

    /// <summary>
    /// Delete user detailed data 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.userDetailsService.DeleteAsync(us => us.Id.Equals(id)));

    /// <summary>
    /// Get user detailed Data 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async ValueTask<ActionResult<UserDetail>> GetAsync([FromRoute] int id)
        => Ok(await this.userDetailsService.GetAsync(us => us.Id.Equals(id)));

    /// <summary>
    /// Get all user details
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<ActionResult<UserDetail>> GetAll(
        [FromQuery] PaginationParams @params) =>
        Ok(await this.userDetailsService.GetAllAsync(@params));

}