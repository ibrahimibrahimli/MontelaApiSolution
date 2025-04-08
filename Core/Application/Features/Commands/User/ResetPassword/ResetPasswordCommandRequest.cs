using MediatR;

namespace Application.Features.Commands.User.ResetPassword
{
    public class ResetPasswordCommandRequest : IRequest<ResetPasswordCommandResponse>
    {
        public string Email { get; set; }
    }
}