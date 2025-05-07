namespace Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignROleEndpointAsync(string[] roles, string endpointCode);
    }
}
