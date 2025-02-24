using BE_Phase1.Data.Repositories;
using BE_Phase1.Models;

namespace BE_Phase1.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;

        public RoleService(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Role>> GetRolesListAsync(string? search, int? permissionId, bool? isActive)
        {
            IEnumerable<Role> roles;

            if (permissionId.HasValue)
            {
                roles = await _roleRepository.GetRolesByPermissionIdAsync(permissionId.Value);
            }
            else if (isActive.HasValue)
            {
                roles = await _roleRepository.GetRolesByActiveStatusAsync(isActive.Value);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                roles = await _roleRepository.GetRolesBySearchAsync(search);
            }
            else
            {
                roles = await _roleRepository.GetAllRolesAsync();
            }

            // Return a list of Role objects
            return roles.Select(r => new Role
            {
                RoleId = r.RoleId,
                Name = r.Name,
                Active = r.Active,
                Permissions = r.Permissions.ToList() // Assuming Permissions is a collection of Permission objects
            });
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);

            if (role == null)
            {
                throw new Exception($"Role with ID {roleId} not found.");
            }

            return new RoleDto
            {
                Id = role.RoleId,
                Name = role.Name,
                Active = role.Active,
                PermissionIds = role.Permissions.Select(p => p.PermissionId).ToList()
            };
        }

        public async Task<int> AddRoleAsync(RoleDto roleDto)
        {
            // Validate PermissionIds
            await ValidatePermissionIdsAsync(roleDto.PermissionIds);

            // Check for duplicate Role Name
            if (await _roleRepository.AnyAsync(r => r.Name == roleDto.Name))
            {
                throw new Exception("Role with the same name already exists.");
            }

            var allPermissions = await _permissionRepository.GetAllPermissionsAsync(); // Await the task here

            var permissions = allPermissions
                                .Where(p => roleDto.PermissionIds.Contains(p.PermissionId))
                                .ToList(); // Get the actual Permission objects

            var role = new Role
            {
                Name = roleDto.Name,
                Active = roleDto.Active,
                Permissions = permissions // Assign the list of Permission objects
            };

            await _roleRepository.AddRoleAsync(role);

            return role.RoleId; // Return the new RoleId
        }

        public async Task<int> UpdateRoleAsync(RoleDto roleDto)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(roleDto.Id);

            if (existingRole == null)
            {
                throw new Exception($"Role with ID {roleDto.Id} not found.");
            }

            // Validate PermissionIds
            await ValidatePermissionIdsAsync(roleDto.PermissionIds);

            // Check for duplicate Role Name
            if (await _roleRepository.AnyAsync(r => r.Name == roleDto.Name && r.RoleId != roleDto.Id))
            {
                throw new Exception("Role with the same name already exists.");
            }

            existingRole.Name = roleDto.Name;
            existingRole.Active = roleDto.Active;

            var allPermissions = await _permissionRepository.GetAllPermissionsAsync(); // Await the task here
            var permissions = allPermissions
                                .Where(p => roleDto.PermissionIds.Contains(p.PermissionId))
                                .ToList(); // Get the actual Permission objects

            existingRole.Permissions = permissions; // Assign the list of Permission objects

            await _roleRepository.UpdateRoleAsync(existingRole);

            return existingRole.RoleId; // Return the updated RoleId
        }

        public async Task<int> DeleteRoleAsync(int roleId)
        {
            var isDeleted = await _roleRepository.DeleteRoleAsync(roleId);

            if (!isDeleted)
            {
                throw new Exception($"Role with ID {roleId} not found or cannot be deleted.");
            }

            return roleId; // Return the deleted RoleId
        }

        private async Task ValidatePermissionIdsAsync(IEnumerable<int> permissionIds)
        {
            var existingPermissions = await _permissionRepository.GetAllPermissionsAsync();
            var invalidPermissionIds = permissionIds.Where(id => !existingPermissions.Any(p => p.PermissionId == id)).ToList();

            if (invalidPermissionIds.Any())
            {
                throw new Exception($"Invalid Permission IDs: {string.Join(", ", invalidPermissionIds)}");
            }
        }
    }
}
