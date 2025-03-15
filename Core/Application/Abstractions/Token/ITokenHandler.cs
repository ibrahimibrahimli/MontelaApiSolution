using Application.DTOs;

namespace Application.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
