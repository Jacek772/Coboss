using Coboss.Application.Services.Abstracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Coboss.Application.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public bool Compare(string password, string hash, byte[] salt)
        {
            return Hash(password, salt) == hash;
        }

        public string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public byte[] RandomSalt(int bytes)
        {
            return RandomNumberGenerator.GetBytes(bytes);
        }
    }
}
