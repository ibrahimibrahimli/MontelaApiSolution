using Application.Abstractions.Services;
using Application.Exceptions.User;
using MediatR;

namespace Application.Features.Commands.User.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.NewPassword.Equals(request.NewPasswordConfirm))
                throw new PasswordChangeFailedException();

            await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.NewPassword);
            return new();
        }
    }
}
