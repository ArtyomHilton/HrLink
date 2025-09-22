using BCrypt.Net;

namespace HrLink.Application.Common;

public static class PasswordHasher
{
    private const int WorkFactor = 13;
    
    public static string HashPassword(string password) => 
        BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, WorkFactor);

    public static bool VerifyPassword(string password, string hashPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword, HashType.SHA512);
}