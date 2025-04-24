using Application.DTOs.Role;

namespace Application.Abstractions.Services
{
    public interface IRoleService
    {
        IDictionary<string, string> GetAllRoles();
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> DeleteRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(string id, string roleName);
    }
}
