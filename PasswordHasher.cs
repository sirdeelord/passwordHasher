using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace passwordHasher;
public class PasswordHasher
{
    // hash password using PBKDF2 with SHA-256 with
    // 128-bit salt and 1000 iterations
    public string HashPassword(string password)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        var hashedPwd = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return $"{ Convert.ToBase64String(salt) }:{ hashedPwd }";
    }

    // verify that the password is correct by comparing it 
    // with the hash of the against the stored hash
    public bool VerifyPassword(string password, string storedHash)
    {
        var salt = Convert.FromBase64String(storedHash.Split(':')[0]);
        var hash = storedHash.Split(':')[1];
        var hashedPwd = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        return hashedPwd == hash;
    }
}
