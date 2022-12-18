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
    
    [HttpPost]
    public async ValueTask<ActionResult<Experience>> CreateAsync([FromBody] ExperienceForCreationDto experienceForCreationDto)
        => Ok(await this.experienceService.CreateAsync(experienceForCreationDto));
    
    [HttpPut]
    public async ValueTask<ActionResult<Experience>> UpdateAsync(int id, [FromBody] ExperienceForCreationDto experienceForCreationDto)
        => Ok(await this.experienceService.UpdateAsync(id, experienceForCreationDto));
    
    [HttpDelete]
    public async ValueTask<ActionResult<bool>> DeleteAsync(int id)
        => Ok(await this.experienceService.DeleteAsync(ex => ex.Id.Equals(id)));
    
    [HttpGet("{id}")]
    public async ValueTask<ActionResult<Experience>> GetAsync([FromRoute] int id)
        => Ok(await this.experienceService.GetAsync(ex => ex.Id.Equals(id)));
    
    [HttpGet]
    public async ValueTask<ActionResult<Experience>> GetAll(
        [FromQuery] PaginationParams @params, string search) =>
        Ok(await this.experienceService.GetAllAsync(@params, search: search));
}