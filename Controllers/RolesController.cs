using Microsoft.AspNetCore.Mvc;
using BE_Phase1.Models;
using BE_Phase1.Services;

[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetRolesList([FromQuery] string? search, [FromQuery] int? permissionId, [FromQuery] bool? isActive)
    {
        try
        {
            var roles = await _roleService.GetRolesListAsync(search, permissionId, isActive);
            return Ok(new { data = roles });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetRole(int id)
    {
        try
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRole(RoleDto roleDto)
    {
        try
        {
            var roleId = await _roleService.AddRoleAsync(roleDto);
            return CreatedAtAction(nameof(GetRole), new { id = roleId }, new { Message = "Added", Id = roleId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(RoleDto roleDto)
    {
        try
        {
            var roleId = await _roleService.UpdateRoleAsync(roleDto);
            return Ok(new { Message = "Updated", Id = roleId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(int id)
    {
        try
        {
            var roleId = await _roleService.DeleteRoleAsync(id);
            return Ok(new { Message = "Deleted", Id = roleId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}