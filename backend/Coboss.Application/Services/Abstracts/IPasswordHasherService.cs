using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IPasswordHasherService
    {
        bool ComparePasswordHash(string password, User user);
        bool ComparePasswordHash(string password, string hash, byte[] salt);
        string HashPassword(string password, byte[] salt);
        byte[] GenerateRandomSalt(int bytes);
    }
}
