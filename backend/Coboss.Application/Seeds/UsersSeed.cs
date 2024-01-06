using Coboss.Application.Configuration;
using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;

namespace Coboss.Application.Seeds
{
    public class UsersSeed : ISeed
    {
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public UsersSeed(IUsersService usersService, IRolesService rolesService, AuthenticationConfiguration authenticationConfiguration)
        {
            _usersService = usersService;
            _rolesService = rolesService;
            _authenticationConfiguration = authenticationConfiguration;
        }

        public async Task Seed()
        {
            await CreateDefaultAdministrator();
        }

        private async Task CreateDefaultAdministrator()
        {
            bool userExists = await _usersService.ExistsAsync(_authenticationConfiguration.AdminEmail);
            if (!userExists)
            {
                User user = new User
                {
                    Email = _authenticationConfiguration.AdminEmail,
                    Password = _authenticationConfiguration.AdminPassword,
                    Role = await _rolesService.GetByNameAsync(_authenticationConfiguration.AdminRoleName)
                };
                await _usersService.CreateAsync(user);
            }
        }

    }
}
