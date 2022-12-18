using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Service.DTOs.Chats;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessagesController : RESTFulController
{
    private readonly IMessageService messageService;
    public MessagesController(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    /// <summary>
    /// Create new Message
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<ActionResult<Message>> CreateAsync(MessageCreationDto dto)
        => Ok(await messageService.CreateAsync(dto));

    /// <summary>
    /// Update existed Message
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async ValueTask<ActionResult<Message>> UpdateAsync([FromRoute] int id, string message)
        => Ok(await messageService.UpdateAsync(id, message));

    /// <summary>
    /// Delete existing Message
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute] int id)
        => Ok(await messageService.DeleteAsync(c => c.Id == id));

    /// <summary>
    /// Get message with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async ValueTask<ActionResult<Message>> GetAsync([FromRoute] int id)
        => Ok(await messageService.GetAsync(c => c.Id == id));

    /// <summary>
    /// Get all messages
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<ActionResult<Message>> GetAll(
            [FromQuery] PaginationParams @params)
        => Ok(await this.messageService.GetAllAsync(@params));
}