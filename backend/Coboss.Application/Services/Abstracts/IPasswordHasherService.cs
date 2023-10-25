using Coboss.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
