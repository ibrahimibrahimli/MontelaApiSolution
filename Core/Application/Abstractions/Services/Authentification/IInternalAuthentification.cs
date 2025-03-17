using Application.DTOs;

namespace Application.Abstractions.Services.Authentification
{
    public interface IInternalAuthentification
    {
        Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifetime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
