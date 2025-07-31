using Application.Wrappers;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Authentification.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<string>>
    {
        readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return Result<string>.Failure("User not found");

            var decodedToken = WebUtility.UrlDecode(request.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if(!result.Succeeded)
                return Result<string>.Failure($"Failed to reset password");

            return Result<string>.Success("", "Password successfully reset");
        }
    }
}
