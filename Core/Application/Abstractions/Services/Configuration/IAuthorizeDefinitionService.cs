using Application.DTOs.Configuration;

namespace Application.Abstractions.Services.Configuration
{
    public interface IAuthorizeDefinitionService
    {
        List<Menu> GetAuthorizeDefinitionEndpoints( Type type);
    }
}
