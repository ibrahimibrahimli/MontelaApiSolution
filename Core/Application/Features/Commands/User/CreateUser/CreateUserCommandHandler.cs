using Application.Abstractions.Services;
using Application.DTOs.User;
using MediatR;

namespace Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           CreateUserResponseDto response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Fullname = request.Fullname,
                Username = request.Username,
                Password = request.Password,
                RePassword = request.RePassword,
            });
            

            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
            //throw new UserCreateFailedException();
        }
    }
}
