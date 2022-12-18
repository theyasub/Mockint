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

        [HttpPost("{id:int}")]
        public async ValueTask<ActionResult<Room>> CreatePrivateChatAsync([FromRoute] int id)
            => Ok(await chatService.CreatePrivateChatAsync(id));

        [HttpDelete("{id:int}")]
        public async ValueTask<ActionResult<bool>> DeletePrivateChatAsync([FromRoute] int id)
            => Ok(await chatService.DeletePrivateChatAsync(id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Room>>> GetAllAsync(
            [FromQuery] PaginationParams @params)
            => Ok(await chatService.GetAllAsync(@params));

        [HttpGet("{id:int}")]
        public async ValueTask<ActionResult<Room>> GetAsync([FromRoute] int id)
            => Ok(await chatService.GetPrivateChatAsync(id));
    }
}
