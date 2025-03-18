using Application.DTOs;
using Domain.Entities.Identity;

namespace Application.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute, AppUser user);
        string CreateRefreshToken();
    }
}
