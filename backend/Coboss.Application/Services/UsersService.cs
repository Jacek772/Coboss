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
        private PasswordHasherService _passwordHasherService;
        private AuthenticationConfiguration _authenticationConfiguration;

        public UsersService(ApplicationDbContext applicationDbContext, PasswordHasherService passwordHasherService, AuthenticationConfiguration authenticationConfiguration)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasherService = passwordHasherService;
            _authenticationConfiguration = authenticationConfiguration;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task CreateUserAsync(User user)
        {
            //using(IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        byte[] salt = _passwordHasherService.RandomSalt(_authenticationConfiguration.SaltSize);
            //        string hash = _passwordHasherService.Hash(user.Password, salt);

            //        //User user = new User
            //        //{
            //        //    Email = createUserCommand.Email,
            //        //    Login = createUserCommand.Login,
            //        //    Password = hash,
            //        //    Salt = salt,
            //        //    Name = createUserCommand.Name,
            //        //    Surname = createUserCommand.Surname,
            //        //};
            //        _applicationDbContext.Users.Add(user);
            //        await _applicationDbContext.SaveChangesAsync();
            //        await transaction.CommitAsync();
            //    }
            //    catch(Exception ex)
            //    {
            //        await transaction.RollbackAsync();
            //        throw new Exception("User create error");
            //    }
            //}
        }
    }
}
