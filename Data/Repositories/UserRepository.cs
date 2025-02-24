using Microsoft.EntityFrameworkCore;
using BE_Phase1.Data;
using System.Linq;
using System.Linq.Expressions;
using BE_Phase1.Models;

namespace BE_Phase1.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetUsersBySearchAsync(string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await GetAllUsersAsync();
            }

            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Name.Contains(search))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int? roleId)
        {
            if (roleId == null)
            {
                return await GetAllUsersAsync();
            }

            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByActiveStatusAsync(bool? isActive)
        {
            if (isActive == null)
            {
                return await GetAllUsersAsync();
            }

            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Active == isActive)
                .ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }
    }
}
