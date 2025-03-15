using Application.DTOs;

namespace Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUSerErrorCommandResponse : LoginUserCommandResponse 
    {
        public string Message { get; set; }
    }
}
