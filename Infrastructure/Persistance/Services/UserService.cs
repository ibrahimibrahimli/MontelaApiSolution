using Application.Abstractions.Services;
using Application.DTOs.User;
using Application.Features.Commands.User.CreateUser;
using Azure.Core;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto user)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.Username,
                Email = user.Email,
                FullName = user.Fullname,
            }, user.Password);

            CreateUserResponseDto response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Succes Created";
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}: {error.Description}";

                }
            }
            return response;
        }
    }
}
