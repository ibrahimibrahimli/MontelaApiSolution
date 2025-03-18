using Application.DTOs;
using Domain.Entities.Identity;

namespace Application.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
