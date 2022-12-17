namespace AbuInt.Service.Helpers;

public class SecurityService
{
    private const string _key = "fed9e0da-2004-41f2-1998-84790ddfdbd9";

    public static bool Verify(string password, string salt, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(salt + password + _key, hash);
    }

    public static string Encrypt(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(salt + password + _key);
    }
}
