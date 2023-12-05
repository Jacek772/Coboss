using Coboss.Application.Configuration;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Coboss.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly AuthenticationConfiguration _authenticationConfiguration;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthService(ApplicationDbContext applicationDbContext,
            AuthenticationConfiguration authenticationConfiguration,
            TokenValidationParameters tokenValidationParameters)
        {
            _applicationDbContext = applicationDbContext;
            _authenticationConfiguration = authenticationConfiguration;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthenticationResultDTO> GenerateTokenAsync(User user)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.JwtKey));
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id",user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Expires = DateTime.Now.AddDays(_authenticationConfiguration.JwtExpireDays),
                Issuer = _authenticationConfiguration.JwtIssuer,
                Audience = _authenticationConfiguration.JwtAudience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            RefreshTokenData refreshTokenData = await CreateRefreshTokenDataAsync(token.Id, user);

            return new AuthenticationResultDTO
            {
                Token = tokenString,
                RefreshToken = refreshTokenData.Token
            };
        }

        public async Task SetRefreshTokenDataUsedAsync(RefreshTokenData refreshTokenData)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    refreshTokenData.Used = true;
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("RefreshTokenData create error");
                }
            }
        }

        public async Task<RefreshTokenData> GetRefreshTokenDataAsync(string refreshToken)
        {
            return await _applicationDbContext.RefreshTokensData.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }

        public async Task<RefreshTokenData> CreateRefreshTokenDataAsync(string tokenId, User user)
        {
            RefreshTokenData refreshTokenData;
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    refreshTokenData = new RefreshTokenData
                    {
                        Token = GenerateRefreshToken(),
                        ExpiryDate = DateTime.UtcNow,
                        JwtId = tokenId,
                        User = user,
                    };

                    await _applicationDbContext.RefreshTokensData.AddAsync(refreshTokenData);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("RefreshTokenData create error");
                }
            }
            return refreshTokenData;
        }

        public string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out SecurityToken validatedToken);
                if(!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
