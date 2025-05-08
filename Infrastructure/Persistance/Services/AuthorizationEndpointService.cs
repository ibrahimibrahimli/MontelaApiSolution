using Application.Abstractions.Services;
using Application.Abstractions.Services.Configuration;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
        readonly RoleManager<AppRole> _roleManager;


        public AuthorizationEndpointService(IAuthorizationEndpointService authorizationEndpointService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IAuthorizeDefinitionService authorizeDefinitionService, IMenuWriteRepository menuWriteRepository, IMenuReadRepository menuReadRepository, RoleManager<AppRole> roleManager)
        {
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _authorizeDefinitionService = authorizeDefinitionService;
            _menuWriteRepository = menuWriteRepository;
            _menuReadRepository = menuReadRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles, string endpointCode, Type type, string menu)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await _menuWriteRepository.AddAsync(_menu);
                await _menuWriteRepository.SaveAsync();
            }


            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.Roles).FirstOrDefaultAsync(e => e.Code == endpointCode && e.Menu.Name == menu);
            if (endpoint is null)
            {
                var action = _authorizeDefinitionService.GetAuthorizeDefinitionEndpoints(type)?
                    .FirstOrDefault(m => m.Name == menu)?
                    .Actions.FirstOrDefault(e => e.Code == endpointCode);

                endpoint = new()
                {
                    Menu = _menu,
                    Code = endpointCode,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                };
                await _endpointWriteRepository.AddAsync(endpoint);
                await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.Roles)
            {
                endpoint.Roles.Remove(role);
            }

            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);
            await _endpointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
            Endpoint? endpoint = await _endpointReadRepository.Table
                .Include(e => e.Roles)
                .Include(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

            List<string> roleNames = endpoint.Roles.Select(r => r.Name).ToList();
            if(roleNames.Count != 0)
            {
                return roleNames;
            }
            return null;
        }
    }
}
