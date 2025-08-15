using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentification.Commands.Disable2Fa
{
    public class Disable2FaCommand : IRequest<Result<string>>
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Code { get; set; } = default!;
    }
}
