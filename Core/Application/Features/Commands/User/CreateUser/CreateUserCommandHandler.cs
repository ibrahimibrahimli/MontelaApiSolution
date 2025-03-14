using Application.Exceptions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                FullName = request.Fullname,
            }, request.Password);

            CreateUserCommandResponse response = new CreateUserCommandResponse() { Succeeded = result.Succeeded};
            if (result.Succeeded)
                response.Message = "Succes Created";
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}: {error.Description}";

                }
            }

            return response;
            //throw new UserCreateFailedException();
        }
    }
}
