using Coboss.Application.Configuration;
using Coboss.Application.Services.Abstracts;
using Coboss.Persistance;
using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext _applicationDbContext;
        private AuthenticationConfiguration _authenticationConfiguration;
        private IPasswordHasherService _passwordHasherService;

        public UsersService(ApplicationDbContext applicationDbContext, AuthenticationConfiguration authenticationConfiguration, IPasswordHasherService passwordHasherService)
        {
            _applicationDbContext = applicationDbContext;
            _authenticationConfiguration = authenticationConfiguration;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _applicationDbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _applicationDbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    user.Salt = _passwordHasherService.GenerateRandomSalt(_authenticationConfiguration.SaltSize);
                    user.Password = _passwordHasherService.HashPassword(user.Password, user.Salt);

                    _applicationDbContext.Users.Add(user);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("User create error");
                }
            }
        }

        public async Task<bool> ExistsUserAsync(string email)
        {
            return await _applicationDbContext.Users
                .Where(x => x.Email == email)
                .AnyAsync();
        }
    }
}
