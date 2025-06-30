using Application.Common.Interfaces;
using Domain.Identity;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrustructure.Services
{
    public class TokenService : ITokenService
    {
        readonly JwtSettings _jwtSettings;
        readonly SymmetricSecurityKey authSigningKey;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        }

        public string GenerateAccessToken(ApplicationUser user, IList<string> roles)
        {
            var authClaims = new List<Claim>
               {
                   new(ClaimTypes.NameIdentifier, user.Id),
                   new(ClaimTypes.Email, user.Email),
                   new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               };

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetimeMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidatorParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principial = tokenHandler.ValidateToken(token, tokenValidatorParameters, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtToken || jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                    return null;

                return principial;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
