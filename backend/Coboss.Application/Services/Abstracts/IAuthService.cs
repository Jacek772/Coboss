using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
