namespace BeverageAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _adminService.GetAllUsers();
        return Ok(users);
    }

    [HttpDelete("user/{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            _adminService.DeleteUser(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found.");
        }
    }

    [HttpPost("reset-password/{id}")]
    public IActionResult ResetPassword(int id, string newPassword)
    {
        try
        {
            _adminService.ResetPassword(id, newPassword);
            return Ok("Password reset successfully.");
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found.");
        }
    }

    [HttpPost("update-email/{id}")]
    public IActionResult UpdateEmail(int id, string newEmail)
    {
        try
        {
            _adminService.UpdateUserEmail(id, newEmail);
            return Ok("Email updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);  // Email already in use
        }
        catch (KeyNotFoundException)
        {
            return NotFound("User not found.");
        }
    }
}
