using System.Linq.Expressions;
using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace AbuInt.Service.Services;

public class ExperienceService : IExperienceService
{
    private readonly IUnitOfWork unitOfWork;

    public ExperienceService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Experience> CreateAsync(ExperienceForCreationDto experienceForCreationDto)
    {
        Experience experience =
            await this.unitOfWork.Experiences.GetAsync(e => e.CompanyName.Equals(experienceForCreationDto.CompanyName));

        if (experience is not null)
            throw new CustomException(400, "Experience already exists");

        experience = experienceForCreationDto.Adapt<Experience>();
        
        experience.Create();
        var res = await this.unitOfWork.Experiences.CreateAsync(experience);
        await unitOfWork.SaveChangesAsync();
        return res;
        
    }

    public async ValueTask<Experience> GetAsync(Expression<Func<Experience, bool>> expression)
    {
        Experience experience = await this.unitOfWork.Experiences.GetAsync(expression);

        if (experience is null)
            throw new CustomException(404, "Experiance not found");

        return experience;
    }

    public async ValueTask<IEnumerable<Experience>> GetAllAsync(PaginationParams @params, Expression<Func<Experience, bool>> expression = null, string search = null)
    {
        var experiance = this.unitOfWork.Experiences.GetAll(expression, isTracking: false);

        if (search is not null)
            experiance = experiance.Where(ex => ex.CompanyName == search);
        
        return await experiance.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<Experience> UpdateAsync(long id, ExperienceForCreationDto experienceForCreationDto)
    {
        Experience experience = await this.unitOfWork.Experiences.GetAsync(ex => ex.Id.Equals(id));

        if (experience is null)
            throw new CustomException(404, "Experiance not found ");

        var mappedExp = experienceForCreationDto.Adapt(experience);

        experience = await this.unitOfWork.Experiences.UpdateAsync(mappedExp);
        await this.unitOfWork.SaveChangesAsync();

        return experience;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Experience, bool>> expression)
    {
        Experience experience = await  this.unitOfWork.Experiences.GetAsync(expression);

        if (experience is null)
            throw new CustomException(404, "Experiance not found");

        await this.unitOfWork.Experiences.DeleteAsync(expression);
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }
}