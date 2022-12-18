using AbuInt.Data.IRepositories;
using AbuInt.Domain.Entities.Users;
using AbuInt.Service.Exceptions;
using AbuInt.Service.Helpers;
using AbuInt.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AbuInt.Service.Services;

public class AuthService : IAuthService
{
    public IUnitOfWork unitOfWork { get; set; }
    public IConfiguration configuration { get; set; }
    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.configuration = configuration;
    }

    public async ValueTask<string> GenerateToken(string username, string password)
    {
        User user = await this.unitOfWork.Users.GetAsync(user =>
            user.Username.Equals(username) || user.Gmail.Equals(username));


        if (user == null)
            throw new CustomException(400, "Login or Password is incorrect");

        if (!SecurityService.Verify(password, user.Salt.ToString(), user.Password))
            throw new CustomException(400, "Login or Password is incorrect");

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        byte[] tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        SecurityTokenDescriptor tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            Issuer = configuration["JWT:Issuer"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescription);

        return tokenHandler.WriteToken(token);
    }
}
