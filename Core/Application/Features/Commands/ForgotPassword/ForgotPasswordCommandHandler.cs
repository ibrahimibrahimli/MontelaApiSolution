using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Application.Features.Authentification.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<string>>
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IMailService _mailService;

        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone)
                ?? await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.EmailOrPhone, cancellationToken);

            if (user is null)
                return Result<string>.Failure("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebUtility.UrlEncode(token);

            var resetLink = $"https://fitcircle.com/reset-password?userId={user.Id}&token={encodedToken}";

            await _mailService.SendAsync(user.Email, "Reset Password", resetLink);

            return Result<string>.Success("Successfully sended password reset link");
        }
    }
}
