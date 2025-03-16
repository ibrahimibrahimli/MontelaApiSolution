using Application.DTOs;

namespace Application.Abstractions.Services.Authentification
{
    public interface IExternalAuthentification
    {
        Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
        Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}
