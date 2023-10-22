using Coboss.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Services.Abstracts
{
    public interface IUsersService
    {
        Task<User> GetUserByLoginAsync(string login);
        Task CreateUserAsync(User user);
    }
}
