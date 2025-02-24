using Microsoft.EntityFrameworkCore;
using BE_Phase1.Data;
using System.Linq;
using System.Linq.Expressions;
using BE_Phase1.Models;

namespace BE_Phase1.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.AsNoTracking().ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                return false;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Role>> GetRolesBySearchAsync(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await GetAllRolesAsync();
            }

            return await _context.Roles
                .Where(r => r.Name.Contains(search))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesByPermissionIdAsync(int? permissionId)
        {
            if (permissionId == null)
            {
                return await GetAllRolesAsync();
            }

            return await _context.Roles
                .Include(r => r.Permissions)
                .Where(r => r.Permissions.Any(p => p.PermissionId == permissionId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesByActiveStatusAsync(bool? isActive)
        {
            if (isActive == null)
            {
                return await GetAllRolesAsync();
            }

            return await _context.Roles
                .Where(r => r.Active == isActive)
                .ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Roles.AnyAsync(predicate);
        }
    }
}
