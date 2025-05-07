using Application.Abstractions.Services;

namespace Persistance.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        public Task AssignROleEndpointAsync(string[] roles, string endpointCode)
        {
            throw new NotImplementedException();
        }
    }
}
