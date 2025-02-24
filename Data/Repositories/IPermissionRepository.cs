using BE_Phase1.Models;

namespace BE_Phase1.Data.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    }
}
