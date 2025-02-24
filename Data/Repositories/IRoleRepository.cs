using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Phase1.Models;

namespace BE_Phase1.Data.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int roleId);
        Task<Role> AddRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int roleId);
        Task<IEnumerable<Role>> GetRolesBySearchAsync(string? search);
        Task<IEnumerable<Role>> GetRolesByPermissionIdAsync(int? permissionId);
        Task<IEnumerable<Role>> GetRolesByActiveStatusAsync(bool? isActive);
        Task<bool> AnyAsync(Expression<Func<Role, bool>> predicate); // Updated signature
    }
}
