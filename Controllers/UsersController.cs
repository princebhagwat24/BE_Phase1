using Microsoft.AspNetCore.Mvc;
using BE_Phase1.Models;
using BE_Phase1.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUsersList([FromQuery] string? search, [FromQuery] int? roleId, [FromQuery] bool? isActive, [FromQuery] int? pageNumber)
    {
        try
        {
            var users = await _userService.GetUsersListAsync(search, roleId, isActive, pageNumber);
            return Ok(new { pageNumber, matchesCount = users.Count(), data = users });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserDto userDto)
    {
        try
        {
            var userId = await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, new { Message = "Added", Id = userId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserDto userDto)
    {
        try
        {
            var userId = await _userService.UpdateUserAsync(userDto);
            return Ok(new { Message = "Updated", Id = userId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var userId = await _userService.DeleteUserAsync(id);
            return Ok(new { Message = "Deleted", Id = userId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}