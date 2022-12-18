using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Company;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CompaniesController : RESTFulController
{
    private readonly ICompanyService companyService;

    public CompaniesController(ICompanyService companyService)
    {
        this.companyService = companyService;
    }

    /// <summary>
    /// Create new Company
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<ActionResult<Company>> CreateAsync([FromForm] CompanyCreationDto dto)
        => Ok(await companyService.CreateAsync(dto));

    /// <summary>
    /// Update existed company
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public async ValueTask<ActionResult<Company>> UpdateAsync([FromRoute] int id, CompanyCreationDto dto)
        => Ok(await companyService.UpdateAsync(id, dto));

    /// <summary>
    /// Delete existing company
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute] int id)
        => Ok(await companyService.DeleteAsync(c => c.Id == id));

    /// <summary>
    /// Get company with given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async ValueTask<ActionResult<Company>> GetAsync([FromRoute] int id)
        => Ok(await companyService.GetAsync(c => c.Id == id));

    /// <summary>
    /// Get all companies
    /// </summary>
    /// <param name="params"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    [HttpGet]
    public async ValueTask<ActionResult<User>> GetAll(
            [FromQuery] PaginationParams @params, string search)
        => Ok(await this.companyService.GetAllAsync(@params, searchText: search));
}
