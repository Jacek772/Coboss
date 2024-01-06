using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IRolesService
    {
        Task<Role> GetByNameAsync(string name);
        Task<bool> ExistsAsync(string name);
        Task CreateAsync(Role user);
    }
}
