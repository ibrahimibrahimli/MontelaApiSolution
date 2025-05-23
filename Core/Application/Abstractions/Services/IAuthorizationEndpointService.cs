﻿namespace Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleEndpointAsync(string[] roles, string endpointCode, Type type, string menu);
        public Task<List<string>> GetRolesToEndpointAsync(string code, string menu);
    }
}
