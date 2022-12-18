using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Service.DTOs.Company;
using System.Linq.Expressions;

namespace AbuInt.Service.Interfaces;

public interface IVacancyService
{
    /// <summary>
    /// Creaing new vacansy 
    /// </summary>
    /// <param name="VacancyCreationDto"></param>
    /// <returns></returns>
    ValueTask<Vacancy> CreateAsync(VacancyForCreationDto vacancyCreationDto);

    /// <summary>
    /// Get one Vacancy info by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<Vacancy> GetAsync(Expression<Func<Vacancy, bool>> expression);

    /// <summary>
    /// Get all Vacancy information with pagenation and can validate data with expresstion and search text
    /// </summary>
    /// <param name="params"></param>
    /// <param name="expression"></param>
    /// <param name="searchText"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<Vacancy>> GetAllAsync(PaginationParams @params, Expression<Func<Vacancy, bool>> expression = null, string searchText = null);

    /// <summary>
    /// Update Vacancy
    /// </summary>
    /// <param name="VacancyCreationDto"></param>
    /// <returns></returns>
    ValueTask<Vacancy> UpdateAsync(int id, VacancyForCreationDto VacancyCreationDto);

    /// <summary>
    /// Delete Vacancy by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    ValueTask<bool> DeleteAsync(Expression<Func<Vacancy, bool>> expression);
}
