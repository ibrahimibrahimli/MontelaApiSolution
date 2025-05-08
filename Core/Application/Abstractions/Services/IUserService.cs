using Application.DTOs.User;
using Domain.Entities.Identity;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto user);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenTime);
        Task UpdatePasswordAsync(string UserId, string resetToken, string newPassword);
        Task<List<UserDto>> GetAllUsers(int page, int size);
        Task AssignRoleToUser(string userId, string[] roles);
        Task<List<string>> GetRolesToUserAsync(string userId);
        int TotalUserCount {  get; }
    }
}
