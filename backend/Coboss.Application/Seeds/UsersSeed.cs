using Coboss.Application.Configuration;
using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services.Abstracts;
using Coboss.Persistance.Entities;

namespace Coboss.Application.Seeds
{
    public class UsersSeed : ISeed
    {
        public readonly AuthenticationConfiguration _authenticationConfiguration;
        public readonly IUsersService _usersService;

        public UsersSeed(IUsersService usersService, AuthenticationConfiguration authenticationConfiguration)
        {
            _usersService = usersService;
            _authenticationConfiguration = authenticationConfiguration;
        }

        public async Task Seed()
        {
            await CreateDefaultAdministrator();
        }

        private async Task CreateDefaultAdministrator()
        {
            bool userExists = await _usersService.ExistsUserAsync(_authenticationConfiguration.AdminLogin);
            if (!userExists)
            {
                User user = new User
                {
                    Login = _authenticationConfiguration.AdminLogin,
                    Password = _authenticationConfiguration.AdminPassword,
                    Email = _authenticationConfiguration.AdminEmail
                };
                await _usersService.CreateUserAsync(user);
            }
        }

    }
}
