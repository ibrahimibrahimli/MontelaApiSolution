 using Application.Abstractions.Services;
using Application.Abstractions.Services.Configuration;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IAuthorizeDefinitionService _authorizeDefinitionService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;


        public AuthorizationEndpointService(IAuthorizationEndpointService authorizationEndpointService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IAuthorizeDefinitionService authorizeDefinitionService, IMenuWriteRepository menuWriteRepository, IMenuReadRepository menuReadRepository)
        {
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _authorizeDefinitionService = authorizeDefinitionService;
            _menuWriteRepository = menuWriteRepository;
            _menuReadRepository = menuReadRepository;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string endpointCode, Type type, string menu)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                await _menuWriteRepository.AddAsync(new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                });
            }
            await _menuWriteRepository.SaveAsync();

           Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == endpointCode &&  e.Menu.Name == menu);
            if(endpoint is null)
            {
                var action = _authorizeDefinitionService.GetAuthorizeDefinitionEndpoints(type)?
                    .FirstOrDefault(m => m.Name == menu)?
                    .Actions.FirstOrDefault(e => e.Code == endpointCode);

                endpoint = new()
                {
                    Code = endpointCode,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                };
            }
            await _endpointWriteRepository.AddAsync(endpoint);
            await _endpointWriteRepository.SaveAsync();

            foreach (var role in roles)
                endpoint.Roles.Add(new()
                {

                });
        }
    }
}
