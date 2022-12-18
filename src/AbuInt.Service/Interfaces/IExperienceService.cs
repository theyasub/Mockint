using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.DTOs.Users;
using System.Linq.Expressions;

namespace AbuInt.Service.Interfaces;

public interface IExperienceService
{
    ValueTask<Experience> CreateAsync(ExperienceForCreationDto experienceForCreationDto);

    ValueTask<Experience> GetAsync(Expression<Func<Experience, bool>> expression);

    ValueTask<IEnumerable<Experience>> GetAllAsync(PaginationParams @params, Expression<Func<Experience, bool>> expression = null, string search = "");

    ValueTask<Experience> UpdateAsync(long id, ExperienceForCreationDto experienceForCreationDto);

    ValueTask<bool> DeleteAsync(Expression<Func<Experience, bool>> expression);

}