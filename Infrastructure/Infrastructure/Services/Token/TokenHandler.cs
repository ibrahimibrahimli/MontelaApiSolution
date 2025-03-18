using Application.Abstractions;
using Application.DTOs;
using Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenHandler : ITokenHandler
    {
        private static IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Token CreateAccessToken(int second, AppUser user)
        {
            Token token = new Token();
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["JWTToken:SecurityKey"]));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken jwtSecurityToken = new(
                audience : _configuration["JWTToken:Audience"],
                issuer : _configuration["JWTToken:Issuer"],
                expires : token.Expiration,
                notBefore : DateTime.UtcNow,
                signingCredentials : signingCredentials,
                claims : new List<Claim> { new(ClaimTypes.Name, user.UserName)}
                );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number= new byte[32]; 
            using RandomNumberGenerator random =  RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
