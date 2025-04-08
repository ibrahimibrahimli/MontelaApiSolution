using Application.Features.Commands.User.FacebookLogin;
using Application.Features.Commands.User.GoogleLogin;
using Application.Features.Commands.User.LoginUser;
using Application.Features.Commands.User.RefreshTokenLogin;
using Application.Features.Commands.User.ResetPassword;
using Application.Features.Commands.User.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenLoginCommandRequest refreshTokenLoginCommand)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommand);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest facebookLoginCommandRequest)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(facebookLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordCommandRequest request)
        {
            ResetPasswordCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("verify-resetToken")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest request)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
