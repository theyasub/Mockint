using AbuInt.Data.IRepositories;
using AbuInt.Data.Repositories;
using AbuInt.Service.Helpers;
using AbuInt.Service.Interfaces;
using AbuInt.Service.Interfaces.Users;
using AbuInt.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace AbuInt.API.Extensions;

public static class ServiceExtensions
{
    #region Add service
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<FIleHelper>();
        services.AddScoped<IMemoryCache, MemoryCache>();
        services.AddScoped<EmailHelper>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IVacancyService, VacancyService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();
    }
    #endregion

    #region Jwt Service
    public static void AddJwtService(this IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection("Jwt");

        string key = jwtSettings.GetSection("Key").Value;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });
    }
    #endregion

    #region Setup Swagger
    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AbuInt.Api", Version = "v1" });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
    #endregion
}
