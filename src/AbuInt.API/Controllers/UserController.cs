using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : RESTFulController
    {
        public IUserService userService { get; set; }
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userForCreationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<ActionResult<User>> CreateAsync(UserForCreationDto userForCreationDto)
            => Ok(await this.userService.CreateAsync(userForCreationDto));

        /// <summary>
        /// Update user's data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userForCreationDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<ActionResult<User>> UpdateAsync(int id, UserForCreationDto userForCreationDto)
            => Ok(await this.userService.UpdateAsync(id, userForCreationDto));


        /// <summary>
        /// Delete user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
            => Ok(await this.userService.DeleteAsync(user => user.Id.Equals(id)));

        /// <summary>
        /// Get user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<ActionResult<User>> GetAsync([FromRoute] int id)
            => Ok(await this.userService.GetAsync(user => user.Id.Equals(id)));

        /// <summary>
        /// Get info of user which is releted himself
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public async ValueTask<ActionResult<User>> GetInfoAsync()
            => Ok(await this.userService.GetInfoAsync());

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="params"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<ActionResult<User>> GetAll(
            [FromQuery] PaginationParams @params, string search) =>
                Ok(await this.userService.GetAllAsync(@params, search: search));
    }
}
