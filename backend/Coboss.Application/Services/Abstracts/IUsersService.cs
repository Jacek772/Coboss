using Coboss.Persistance.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IUsersService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByLoginAsync(string login);
        Task<bool> ExistsUserAsync(string login);
        Task CreateUserAsync(User user);
    }
}
