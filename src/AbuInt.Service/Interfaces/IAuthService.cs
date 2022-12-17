namespace AbuInt.Service.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Generate Token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    ValueTask<string> GenerateToken(string username, string password);
}
