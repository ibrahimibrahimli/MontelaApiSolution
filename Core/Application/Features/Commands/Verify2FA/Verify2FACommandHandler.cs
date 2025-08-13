using Application.Wrappers;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Authentification.Commands.Verify2FA
{
    public class Verify2FACommandHandler : IRequestHandler<Verify2FACommand, Result<string>>
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly ILogger<Verify2FACommandHandler> _logger;

        public Verify2FACommandHandler(UserManager<ApplicationUser> userManager, ILogger<Verify2FACommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(Verify2FACommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogWarning("2FaVerify : User not found");
                return Result<string>.Failure("User not found");
            }

            if (string.IsNullOrEmpty(user.TwoFactorCode) || user.TwoFactorCodeGeneratedAt == null)
            {
                _logger.LogWarning("2FaVerify : No Verification code");
                return Result<string>.Failure("No Verification code");
            }

            var timeElapsed = DateTime.UtcNow - user.TwoFactorCodeGeneratedAt.Value;
            if (timeElapsed.TotalMinutes > 5)
            {
                _logger.LogWarning("2FaVerify : Verification code expired");
                return Result<string>.Failure("Verification code expired");
            }

            if (user.TwoFactorCode != request.Code)
            {
                _logger.LogWarning("2FaVerify : Invalid verification code");
                return Result<string>.Failure("Invalid verification code"); 
            }

            user.TwoFactorEnabled = true;

            user.TwoFactorCode = null;
            user.TwoFactorCodeGeneratedAt = null;

            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.UpdateAsync(user);

            return Result<string>.Success("", "Two-factor authentication successfully verified and enabled.");
        }
    }
}
