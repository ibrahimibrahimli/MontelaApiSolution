using Application.Abstractions.Services.Authentification;

namespace Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentification, IInternalAuthentification
    {
    }
}
