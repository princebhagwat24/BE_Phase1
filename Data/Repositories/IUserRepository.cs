using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Phase1.Models;

namespace BE_Phase1.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int userId);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<IEnumerable<User>> GetUsersBySearchAsync(string? search);
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int? roleId);
        Task<IEnumerable<User>> GetUsersByActiveStatusAsync(bool? isActive);
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate); // Updated signature
    }
}
