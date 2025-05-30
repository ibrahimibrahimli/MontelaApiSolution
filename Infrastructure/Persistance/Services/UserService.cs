﻿using Application.Abstractions.Services;
using Application.DTOs.User;
using Application.Exceptions.User;
using Application.Helpers;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IEndpointReadRepository _endpointReadRepository;

        public UserService(UserManager<AppUser> userManager, IEndpointReadRepository readRepository, IEndpointReadRepository endpointReadRepository)
        {
            _userManager = userManager;
            _endpointReadRepository = endpointReadRepository;
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

        public async Task<List<UserDto>> GetAllUsers(int page, int size)
        {
            List<AppUser> users = await _userManager.Users.Skip(page * size).Take(size).ToListAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Fullname = user.FullName,
                UserName = user.UserName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                Email = user.Email,
            }).ToList();
        }
        public int TotalUserCount => _userManager.Users.Count();


        public async Task UpdatePasswordAsync(string UserId, string resetToken, string newPassword)
        {
            AppUser? user = await _userManager.FindByIdAsync(UserId);
            if (user is not null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenTime)
        {
            if (user is not null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenTime);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new NotFoundUserException();
        }

        public async Task AssignRoleToUser(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<List<string>> GetRolesToUserAsync(string userIdOrName)
        {
            AppUser? user = await _userManager.FindByIdAsync(userIdOrName);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }

            if (user is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToList();
            }
            return null;
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            var userRoles = await GetRolesToUserAsync(name);
            if (!userRoles.Any())
                return false;

            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);
            if (endpoint == null)
                return false;

            var endpointRoles = endpoint.Roles.Select(r => r.Name);

            foreach (var userRole in userRoles)
            {
                foreach (var endpointRole in endpointRoles)
                    if (userRole == endpointRole)
                        return true;

            }
            return false;
        }
    }
}
