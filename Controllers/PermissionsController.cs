using BE_Phase1.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BE_Phase1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPermissionsList()
        {
            try
            {
                var permissions = await _permissionRepository.GetAllPermissionsAsync();
                return Ok(new { data = permissions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
