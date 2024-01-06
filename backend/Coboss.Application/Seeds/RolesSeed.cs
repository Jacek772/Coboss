using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;

namespace Coboss.Application.Seeds
{
    public class RolesSeed : ISeed
    {
        private readonly Role[] _roles = new Role[] 
        {
            new Role() { Name = "Administrator" },
            new Role() { Name = "Manager" },
            new Role() { Name = "Employee" }
        };

        private readonly IRolesService _rolesService;

        public RolesSeed(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        public async Task Seed()
        {
            foreach(Role role in _roles)
            {
                bool roleExists = await _rolesService.ExistsAsync(role.Name);
                if(!roleExists)
                {
                    await _rolesService.CreateAsync(role);
                }
            }
        }
    }
}
