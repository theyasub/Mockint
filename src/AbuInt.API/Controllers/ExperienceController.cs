using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperienceController : RESTFulController
{
    private readonly IExperienceService experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        this.experienceService = experienceService;
    }

    /// <summary>
    /// Create experience
    /// </summary>
    /// <param name="experienceForCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<ActionResult<Experience>> CreateAsync([FromBody] ExperienceForCreationDto experienceForCreationDto)
        => Ok(await this.experienceService.CreateAsync(experienceForCreationDto));

    /// <summary>
    /// Update existed experience.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="experienceForCreationDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async ValueTask<ActionResult<Experience>> UpdateAsync(int id, [FromBody] ExperienceForCreationDto experienceForCreationDto)
        => Ok(await this.experienceService.UpdateAsync(id, experienceForCreationDto));

    /// <summary>
    /// Delete experience
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.experienceService.DeleteAsync(ex => ex.Id.Equals(id)));

    /// <summary>
    /// Get experience data which given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async ValueTask<ActionResult<Experience>> GetAsync([FromRoute] int id)
        => Ok(await this.experienceService.GetAsync(ex => ex.Id.Equals(id)));

    /// <summary>
    /// Get all experiences
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<ActionResult<Experience>> GetAll(
        [FromQuery] PaginationParams @params, string search) =>
        Ok(await this.experienceService.GetAllAsync(@params, search: search));
}