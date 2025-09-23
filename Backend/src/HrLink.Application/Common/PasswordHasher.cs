using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace HrLink.Application.Common;

public static class PasswordHasher
{
    private const int WorkFactor = 13;
    private const int SaltSize = 16;
    private const int HashSize = 32;

    public static string HashPassword(string password)
    {
        var salt = GenerateSalt();

        var hash = CreateHash(password, salt);

        var hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string hashPassword)
    {
        var hashBytes = Convert.FromBase64String(hashPassword);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var hash = new byte[HashSize];
        Array.Copy(hashBytes, SaltSize, hash, 0, HashSize);

        var validateHash = CreateHash(password, salt);

        return hash.SequenceEqual(validateHash);
    }

    private static byte[] CreateHash(string password, byte[] salt )
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        
        argon2.MemorySize = 65536;
        argon2.Iterations = 4;
        argon2.DegreeOfParallelism = 4;
        argon2.Salt = salt;

        return argon2.GetBytes(HashSize);
    }
    
    private static byte[] GenerateSalt()
    {
        var salt = new byte[SaltSize];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(salt);

        return salt;
    }
}