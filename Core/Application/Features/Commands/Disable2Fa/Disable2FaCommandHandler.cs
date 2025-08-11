using Application.Wrappers;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Authentification.Commands.Disable2Fa
{
    public class Disable2FaCommandHandler : IRequestHandler<Disable2FaCommand, Result<string>>
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly ILogger<Disable2FaCommandHandler> _logger;

        public Disable2FaCommandHandler(UserManager<ApplicationUser> userManager, ILogger<Disable2FaCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(Disable2FaCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user is null)
            {
                _logger.LogWarning("Disable2FA : User not found");
                return Result<string>.Failure("User not found");
            }

            if (!user.TwoFactorEnabled)
            {
                _logger.LogWarning("Disable2FA : Two-factor authentication is not enabled.");
                return Result<string>.Failure("Two-factor authentication is not enabled.");
            }

            if(user.TwoFactorCode != request.Code)
            {
                _logger.LogInformation("Disabling 2FA authentication failed");
                return Result<string>.Failure("Disabling 2FA authentication failed");
            }

            user.TwoFactorEnabled = false;
            user.TwoFactorCode = null;
            user.TwoFactorCodeGeneratedAt = null;

            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("2FA authentication disabled");
            return Result<string>.Success(string.Empty, "2FA authentication disabled");
        }
    }
}
