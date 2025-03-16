using Application.Abstractions;
using Application.DTOs;
using Application.DTOs.Facebook;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Application.Features.Commands.User.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;

        public FacebookLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=1157685052172854&client_secret=82930b430fbe129289a8d5d2ad53da49&grant_type=client_credentials");
            FacebookTokenDto facebookTokenDto = JsonSerializer.Deserialize<FacebookTokenDto>(accessTokenResponse);
            Console.WriteLine(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient
                .GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookTokenDto.AccessToken}");

            FacebookUserAccessTokenValidator userAccessTokenValidator =
                JsonSerializer.Deserialize<FacebookUserAccessTokenValidator>(userAccessTokenValidation);

            if (userAccessTokenValidator.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");
                FacebookUserInfoResponse response = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var userLoginInfo = new UserLoginInfo("FACEBOOK", userAccessTokenValidator.Data.UserId, "FACEBOOK");

                AppUser? user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                bool result = user != null;

                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(response.Email);
                    if (user == null)
                    {
                        user = new AppUser()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = response.Email,
                            FullName = response.Name,
                            UserName = response.Email

                        };
                        var identityResult = await _userManager.CreateAsync(user);
                        result = identityResult.Succeeded;
                    }
                }
                if (result)
                {
                    await _userManager.AddLoginAsync(user, userLoginInfo);

                    Token token = _tokenHandler.CreateAccessToken(15);
                    return new()
                    {
                        Token = token,
                    };
                }

            }

            throw new Exception("Invalid external authentification");
        }
    }
}
