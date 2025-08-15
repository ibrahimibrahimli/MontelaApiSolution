using Application.Common.Interfaces;
using Application.Common.Models.Email;
using Application.Wrappers;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Authentification.Commands.Enable2FA
{
    public class Enable2FACommandHandler : IRequestHandler<Enable2FACommand, Result<string>>
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IMailService _mailService;
        readonly ILogger<Enable2FACommandHandler> _logger;
        public Enable2FACommandHandler(UserManager<ApplicationUser> userManager, IMailService mailService, ILogger<Enable2FACommandHandler> logger)
        {
            _userManager = userManager;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(Enable2FACommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                _logger.LogWarning("2FA Authentication : User not found");
                return Result<string>.Failure("User not found");
            }

            string code = new Random().Next(100000, 999999).ToString();
            user.TwoFactorCode = code;
            user.TwoFactorCodeGeneratedAt = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            var subject = "Your 2FA Code";
            var body = $"<h2>2FA Verification</h2><p>Your verification code is: <strong>{code}</strong></p>";

            await _mailService.SendAsync(new MailRequest
            {
                To = user.Email,
                Subject = subject,
                HtmlBody = body
            });

            _logger.LogInformation($"2FA Authentication code sended {user.Email}");
            return Result<string>.Success("", $"2FA Authentication code sended {user.Email}");
        }
    }
}
