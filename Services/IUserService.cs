using BE_Phase1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Phase1.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsersListAsync(string? search, int? roleId, bool? isActive, int? pageNumber);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<int> AddUserAsync(UserDto userDto);
        Task<int> UpdateUserAsync(UserDto userDto);
        Task<int> DeleteUserAsync(int userId);
    }
}