using Application.Wrappers;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Authentification.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<Result<string>>
    {
        [Required]
        public string EmailOrPhone { get; set; } = default!;    
    }
}
