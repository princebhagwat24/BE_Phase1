using BE_Phase1.Models;

namespace BE_Phase1.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesListAsync(string? search, int? permissionId, bool? isActive);
        Task<RoleDto> GetRoleByIdAsync(int roleId);
        Task<int> AddRoleAsync(RoleDto roleDto);
        Task<int> UpdateRoleAsync(RoleDto roleDto);
        Task<int> DeleteRoleAsync(int roleId);
    }
}
