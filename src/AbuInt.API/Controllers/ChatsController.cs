using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsController : RESTFulController
    {
        private readonly IChatService chatService;

        public ChatsController(IChatService chatService)
        {
            this.chatService = chatService;
        }


        /// <summary>
        /// Create private chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id:int}")]
        public async ValueTask<ActionResult<Room>> CreatePrivateChatAsync([FromRoute] int id)
            => Ok(await chatService.CreatePrivateChatAsync(id));


        /// <summary>
        /// Delete existed private chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<bool>> DeletePrivateChatAsync([FromRoute] int id)
            => Ok(await chatService.DeletePrivateChatAsync(id));

        /// <summary>
        /// get all private chats
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Room>>> GetAllAsync(
            [FromQuery] PaginationParams @params)
            => Ok(await chatService.GetAllAsync(@params));

        /// <summary>
        /// get private chat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async ValueTask<ActionResult<Room>> GetAsync([FromRoute] int id)
            => Ok(await chatService.GetPrivateChatAsync(id));
    }
}
