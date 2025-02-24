using BE_Phase1.Data.Repositories;
using BE_Phase1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Phase1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUsersListAsync(string? search, int? roleId, bool? isActive, int? pageNumber)
        {
            IEnumerable<User> users;

            if (roleId.HasValue)
            {
                users = await _userRepository.GetUsersByRoleIdAsync(roleId);
            }
            else if (isActive.HasValue)
            {
                users = await _userRepository.GetUsersByActiveStatusAsync(isActive.Value);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                users = await _userRepository.GetUsersBySearchAsync(search);
            }
            else
            {
                users = await _userRepository.GetAllUsersAsync();
            }

            // Convert users to UserDto
            return users.Select(u => new UserDto
            {
                Id = u.UserId,
                Name = u.Name,
                Active = u.Active,
                RoleId = u.RoleId,
                Role = u.Role.Name // Assuming Role is not null; consider adding null checks if necessary
            });
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            return new UserDto
            {
                Id = user.UserId,
                Name = user.Name,
                Active = user.Active,
                RoleId = user.RoleId,
                Role = user.Role.Name // Assuming Role is not null
            };
        }

        public async Task<int> AddUserAsync(UserDto userDto)
        {
            // Check for duplicate User Name
            if (await _userRepository.AnyAsync(u => u.Name == userDto.Name))
            {
                throw new Exception("User with the same name already exists.");
            }

            var user = new User
            {
                Name = userDto.Name,
                Active = userDto.Active,
                RoleId = userDto.RoleId
            };

            await _userRepository.AddUserAsync(user);
            return user.UserId;
        }

        public async Task<int> UpdateUserAsync(UserDto userDto)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userDto.Id);

            if (existingUser == null)
            {
                throw new Exception($"User with ID {userDto.Id} not found.");
            }

            existingUser.Name = userDto.Name;
            existingUser.Active = userDto.Active;
            existingUser.RoleId = userDto.RoleId;

            await _userRepository.UpdateUserAsync(existingUser);
            return existingUser.UserId;
        }

        public async Task<int> DeleteUserAsync(int userId)
        {
            var isDeleted = await _userRepository.DeleteUserAsync(userId);

            if (!isDeleted)
            {
                throw new Exception($"User with ID {userId} not found or cannot be deleted.");
            }

            return userId;
        }
    }
}
