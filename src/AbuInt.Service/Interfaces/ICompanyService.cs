using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Service.DTOs.Company;
using System.Linq.Expressions;

namespace AbuInt.Service.Interfaces;

public interface ICompanyService
{
    /// <summary>
    /// Creaing new company 
    /// </summary>
    /// <param name="companyCreationDto"></param>
    /// <returns></returns>
    public ValueTask<Company> CreateAsync(CompanyCreationDto companyCreationDto);

    /// <summary>
    /// Get one company info by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public ValueTask<Company> GetAsync(Expression<Func<Company, bool>> expression);

    /// <summary>
    /// Get all company information with pagenation and can validate data with expresstion and search text
    /// </summary>
    /// <param name="params"></param>
    /// <param name="expression"></param>
    /// <param name="searchText"></param>
    /// <returns></returns>
    public ValueTask<IEnumerable<Company>> GetAllAsync(PaginationParams @params, Expression<Func<Company, bool>> expression = null, string searchText = null);

    /// <summary>
    /// Update company
    /// </summary>
    /// <param name="companyCreationDto"></param>
    /// <returns></returns>
    public ValueTask<Company> UpdateAsync(int id, CompanyCreationDto companyCreationDto);

    /// <summary>
    /// Delete company by given expression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression);
}

