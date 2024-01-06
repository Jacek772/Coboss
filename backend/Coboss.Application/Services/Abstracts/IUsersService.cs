using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IUsersService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(string email);
        Task CreateAsync(User user);
    }
}
