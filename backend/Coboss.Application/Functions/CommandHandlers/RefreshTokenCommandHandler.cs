using Coboss.Application.Functions.Commands;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Types.DTO;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Coboss.Application.Functions.CommandHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticationResultDTO>
    {
        private readonly IUsersService _usersService;
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IUsersService usersService, 
            IAuthService authService)
        {
            _usersService = usersService;
            _authService = authService;
        }

        public async Task<AuthenticationResultDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            ClaimsPrincipal? claimsPrincipal = _authService.GetPrincipalFromToken(request.Token);
            if (claimsPrincipal == null)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "Invalid Token"
                };
            }

            long expiryDate = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            DateTime expiryDateTimeUTC = new DateTime(1970, 1, 1, 0, 0, 0, 0)
                .AddSeconds(expiryDate);

            if (expiryDateTimeUTC > DateTime.UtcNow)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "This token hasn't expired yet"
                };
            }


            RefreshTokenData refreshTokenData = await _authService.GetRefreshTokenDataAsync(request.RefreshToken);
            if (refreshTokenData == null)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "This refresh token does not exist"
                };
            }

            if (DateTime.UtcNow < refreshTokenData.ExpiryDate)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "This refresh token has expired"
                };
            }

            if (refreshTokenData.Used)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "This refresh token has been used"
                };
            }

            string jti = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (refreshTokenData.JwtId != jti)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "This refresh token does not match this JWT"
                };
            }

            await _authService.SetRefreshTokenDataUsedAsync(refreshTokenData);

            Guid userId = Guid.Parse(claimsPrincipal.Claims.SingleOrDefault(x => x.Type == "Id").Value);
            User user = await _usersService.GetUserByIdAsync(userId);
            if (user is null)
            {
                return new AuthenticationResultDTO
                {
                    Success = false,
                    Error = "Invalid client request"
                };
            }

            return await _authService.GenerateTokenAsync(user);
        }
    }
}
