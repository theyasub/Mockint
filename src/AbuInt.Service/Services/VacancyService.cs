using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Companies;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class VacancyService : IVacancyService
{
    private readonly IUnitOfWork unitOfWork;

    public VacancyService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Vacancy> CreateAsync(VacancyForCreationDto vacancyCreationDto)
    {
        if (await unitOfWork.Companies.GetAsync(c => c.Id == vacancyCreationDto.CompanyId) is null)
            throw new CustomException(400, "Company not found.");

        User hrUserData = await unitOfWork.Users.GetAsync(u => u.Id == vacancyCreationDto.HRId);
        if (hrUserData is null || hrUserData.Role is not Domain.Enums.Role.HR)
            throw new CustomException(400, "Hr Speshilist not found");

        Vacancy mappedVacancy = vacancyCreationDto.Adapt<Vacancy>();

        mappedVacancy.Create();

        Vacancy newVacancy = await unitOfWork.Vacancies.CreateAsync(mappedVacancy);
        await unitOfWork.SaveChangesAsync();

        return newVacancy;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Vacancy, bool>> expression)
    {
        if (await unitOfWork.Vacancies.DeleteAsync(expression))
        {
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        else
            throw new CustomException(400, "Company not found.");
    }

    public async ValueTask<IEnumerable<Vacancy>> GetAllAsync(PaginationParams @params, Expression<Func<Vacancy, bool>> expression = null, string searchText = null)
    {
        IQueryable<Vacancy> vacancies = unitOfWork.Vacancies.GetAll(expression, new string[] { "Company", "User" }, false);

        if (searchText is not null)
            vacancies = vacancies.Where(v => v.Company.Name == searchText);

        return await vacancies.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<Vacancy> GetAsync(Expression<Func<Vacancy, bool>> expression)
        => await unitOfWork.Vacancies.GetAsync(expression) ?? throw new CustomException(404, "Vacancy not found.");

    public async ValueTask<Vacancy> UpdateAsync(int id, VacancyForCreationDto vacancyCreationDto)
    {
        Vacancy existVacancy = await unitOfWork.Vacancies.GetAsync(v => v.Id == id);

        if (existVacancy is null)
            throw new CustomException(400, "Vacancy not found");

        if (await unitOfWork.Companies.GetAsync(c => c.Id == vacancyCreationDto.CompanyId) is null)
            throw new CustomException(400, "Company not found.");

        User hrUserData = await unitOfWork.Users.GetAsync(u => u.Id == vacancyCreationDto.HRId);
        if (hrUserData is null || hrUserData.Role is not Domain.Enums.Role.HR)
            throw new CustomException(400, "Hr Speshilist not found");

        existVacancy = vacancyCreationDto.Adapt(existVacancy);

        existVacancy.Update();
        existVacancy = await unitOfWork.Vacancies.UpdateAsync(existVacancy);
        await unitOfWork.SaveChangesAsync();

        return existVacancy;
    }
}
