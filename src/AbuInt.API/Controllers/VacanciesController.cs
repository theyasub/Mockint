using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Service.DTOs.Company;
using AbuInt.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace AbuInt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : RESTFulController
    {
        public IVacancyService vacancyService { get; set; }
        public VacanciesController(IVacancyService vacancyService)
        {
            this.vacancyService = vacancyService;
        }

        /// <summary>
        /// Create vacancy
        /// </summary>
        /// <param name="vacancyForCreationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<ActionResult<Vacancy>> CreateAsync(VacancyForCreationDto vacancyForCreationDto)
            => Ok(await this.vacancyService.CreateAsync(vacancyForCreationDto));

        /// <summary>
        /// Update vacancy by giving id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vacancyForCreationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<ActionResult<Vacancy>> UpdateAsync(int id, VacancyForCreationDto vacancyForCreationDto)
            => Ok(await this.vacancyService.UpdateAsync(id, vacancyForCreationDto));

        /// <summary>
        /// Delete vacancy by giving id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async ValueTask<ActionResult<Vacancy>> DeleteAsync(int id)
            => Ok(await this.vacancyService.DeleteAsync(vacancy => vacancy.Id.Equals(id)));

        /// <summary>
        /// Get a vacancy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async ValueTask<ActionResult<Vacancy>> GetAsync(int id)
            => Ok(await this.vacancyService.GetAsync(vacancy => vacancy.Id.Equals(id)));

        /// <summary>
        /// Get all vacancies
        /// </summary>
        /// <param name="params"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<ActionResult<Vacancy>> GetAll(
            [FromQuery] PaginationParams @params, string search) =>
                Ok(await this.vacancyService.GetAllAsync(@params, searchText: search));

    }
}
