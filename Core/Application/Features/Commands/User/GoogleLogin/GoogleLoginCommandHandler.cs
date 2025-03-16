using Application.Abstractions;
using Application.DTOs;
using Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Application.Features.Commands.User.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new ValidationSettings()
            {
                Audience = new List<string> { "439063319944-rd4m3eh2r1a3uv2vbft57ngeig2tsrq8.apps.googleusercontent.com" }
            };
            Payload payload = await ValidateAsync(request.IdToken, settings);
            var userLoginInfo = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

            AppUser? user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        FullName = payload.Name,
                        UserName = payload.Email

                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new InvalidOperationException();

            Token token = _tokenHandler.CreateAccessToken(15);

            return new () 
            {
                Token = token
            };
        }
    }
}
