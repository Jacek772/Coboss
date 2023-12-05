using Coboss.Core.Entities;
using Coboss.Types.DTO;
using System.Security.Claims;

namespace Coboss.Application.Services.Abstracts
{
    public interface IAuthService
    {
        Task<AuthenticationResultDTO> GenerateTokenAsync(User user);
        ClaimsPrincipal? GetPrincipalFromToken(string token);
        Task<RefreshTokenData> CreateRefreshTokenDataAsync(string tokenId, User user);
        Task<RefreshTokenData> GetRefreshTokenDataAsync(string refreshToken);
        public string GenerateRefreshToken();
        Task SetRefreshTokenDataUsedAsync(RefreshTokenData refreshTokenData);
    }
}
