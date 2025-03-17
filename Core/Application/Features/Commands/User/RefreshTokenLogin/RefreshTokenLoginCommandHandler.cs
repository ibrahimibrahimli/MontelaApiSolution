using Application.Abstractions.Services;
using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.User.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
          Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token,
            };
        }
    }
}
