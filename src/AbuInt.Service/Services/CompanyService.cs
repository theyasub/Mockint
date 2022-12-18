using AbuInt.Data.IRepositories;
using AbuInt.Domain.Configuration;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Enums;
using AbuInt.Service.DTOs.Company;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Extensions;
using AbuInt.Service.Helpers;
using AbuInt.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace AbuInt.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly FIleHelper fileHelper;

    public CompanyService(IUnitOfWork unitOfWork, FIleHelper fileHelper)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelper = fileHelper;
    }

    public async ValueTask<Company> CreateAsync(CompanyCreationDto companyCreationDto)
    {
        Company existCompany = await unitOfWork.Companies
            .GetAsync(c => c.Name.Equals(companyCreationDto.Name));

        if (existCompany is not null)
            throw new CustomException(400, "This Company is already exist");

        if (HttpContextHelper.UserRole != Enum.GetName(typeof(Role), Role.Admin))
            throw new CustomException(403, "Permission denyed for creating a new company");

        Company newCompanyData = companyCreationDto.Adapt<Company>();

        if (companyCreationDto.Image is not null)
        {
            var attachmentData = await fileHelper.SaveAsync(companyCreationDto.Image);

            Asset newAsset = new Asset()
            {
                Name = attachmentData.fileName,
                Path = attachmentData.filePath
            };

            newAsset.Create();

            newAsset = await unitOfWork.Assets.CreateAsync(newAsset);

            newCompanyData.ImageId = newAsset.Id;
        }

        newCompanyData.Create();

        Company newCompany = await unitOfWork.Companies.CreateAsync(newCompanyData);
        await unitOfWork.SaveChangesAsync();

        return newCompany;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression)
    {
        Company existCompany = await unitOfWork.Companies
            .GetAsync(expression);

        if (existCompany is null)
            throw new CustomException(400, "This Company is exist not exist.");

        if (existCompany.ImageId is not null)
        {
            string imageRelativePath = (await unitOfWork.Assets.GetAsync(a => a.Id == existCompany.ImageId)).Path;
            await fileHelper.DeleteAsync(imageRelativePath);
            await unitOfWork.Assets.DeleteAsync(a => a.Id == existCompany.ImageId);
        }

        bool isSuccessfullyDeleted = await unitOfWork.Companies.DeleteAsync(expression);
        
        await unitOfWork.SaveChangesAsync();

        return isSuccessfullyDeleted;
    }

    public async ValueTask<IEnumerable<Company>> GetAllAsync(PaginationParams @params, Expression<Func<Company, bool>> expression = null, string searchText = null)
    {
        IQueryable<Company> filteredCompanies = unitOfWork.Companies.GetAll(expression, isTracking: false);

        if (searchText is not null)
            filteredCompanies = filteredCompanies.Where(c => c.Name.Contains(searchText));

        return await filteredCompanies.ToPagedList(@params).ToListAsync();
    }

    public async ValueTask<Company> GetAsync(Expression<Func<Company, bool>> expression)
    {
        Company existCompany = await unitOfWork.Companies
            .GetAsync(expression);

        if (existCompany is null)
            throw new CustomException(404, "This Company is not found.");

        return existCompany;
    }

    public async ValueTask<Company> UpdateAsync(int id, CompanyCreationDto companyCreationDto)
    {
        Company existCompany = await unitOfWork.Companies
            .GetAsync(c => c.Id == id);

        if (existCompany is null)
            throw new CustomException(404, "This Company is not found.");

        Company newCompanyChecking = await unitOfWork.Companies
            .GetAsync(c => c.Name.Equals(companyCreationDto.Name));

        if (newCompanyChecking is not null)
            throw new CustomException(400, "This Company is already exist");

        if (HttpContextHelper.UserRole != Enum.GetName(typeof(Role), Role.Admin))
            throw new CustomException(403, "Permission denyed for creating a new company");

        if (existCompany.ImageId is not null && companyCreationDto.Image is not null)
        {
            string imageRelativePath = (await unitOfWork.Assets.GetAsync(a => a.Id == existCompany.ImageId)).Path;
            await fileHelper.DeleteAsync(imageRelativePath);
            await unitOfWork.Assets.DeleteAsync(a => a.Id == existCompany.ImageId);
        }

        existCompany = companyCreationDto.Adapt(existCompany);

        if (companyCreationDto.Image is not null)
        {
            var attachmentData = await fileHelper.SaveAsync(companyCreationDto.Image);

            Asset newAsset = new Asset()
            {
                Name = attachmentData.fileName,
                Path = attachmentData.filePath
            };

            newAsset.Create();

            newAsset = await unitOfWork.Assets.CreateAsync(newAsset);

            existCompany.ImageId = newAsset.Id;
        }

        existCompany = await unitOfWork.Companies.UpdateAsync(existCompany);

        await unitOfWork.SaveChangesAsync();

        return existCompany;
    }
}
