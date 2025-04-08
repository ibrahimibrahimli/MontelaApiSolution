using MediatR;

namespace Application.Features.Commands.User.VerifyResetToken
{
    public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
    }
}