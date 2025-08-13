using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentification.Commands.Verify2FA
{
    public class Verify2FACommand : IRequest<Result<string>>
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Code { get; set; } = default!;
    }
}
