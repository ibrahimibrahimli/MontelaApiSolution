using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentification.Commands.Enable2FA
{
    public class Enable2FACommand : IRequest<Result<string>>
    {
        [Required]
        public string Email { get; set; } = default!;
    }
}
