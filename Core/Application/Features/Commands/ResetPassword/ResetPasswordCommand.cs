using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentification.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result<string>>
    {
        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public string Token { get; set; } = default!;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = default!;
    }
}
