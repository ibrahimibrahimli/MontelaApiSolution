using Application.DTOs.User;
using Domain.Entities.Identity;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto user);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenTime);
    }
}
