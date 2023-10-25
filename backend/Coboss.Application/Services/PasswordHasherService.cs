using Coboss.Application.Services.Abstracts;
using Coboss.Persistance.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Coboss.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public bool ComparePasswordHash(string password, User user)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            return ComparePasswordHash(password, user.Password, user.Salt);
        }

        public bool ComparePasswordHash(string password, string hash, byte[] salt)
        {
            return HashPassword(password, salt) == hash;
        }

        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public byte[] GenerateRandomSalt(int bytes)
        {
            return RandomNumberGenerator.GetBytes(bytes);
        }
    }
}
