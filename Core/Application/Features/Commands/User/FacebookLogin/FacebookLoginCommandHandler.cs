using Application.Abstractions.Services;
using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.User.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
           Token token = await _authService.FacebookLoginAsync(request.AuthToken, 30);
            return new()
            {
                Token = token,
            };
        }
    }
}
