using Coboss.Application.Configuration;
using Coboss.Application.Services.Abstracts;
using Coboss.Persistance;
using Coboss.Persistance.Entities;
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

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
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

        public async Task<bool> ExistsUserAsync(string login)
        {
            return await _applicationDbContext.Users
                .Where(x => x.Login == login)
                .AnyAsync();
        }
    }
}
