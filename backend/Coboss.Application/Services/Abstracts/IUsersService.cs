using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IUsersService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ExistsUserAsync(string email);
        Task CreateUserAsync(User user);
    }
}
