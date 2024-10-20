using BeverageAPI.Services;

namespace BeverageAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO dto)
    {
        try
        {
            var token = _authService.Login(dto.Email, dto.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials.");
        }
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDTO dto)
    {
        try
        {
            _authService.Register(dto.Email, dto.Password, dto.CustomerId);
            return Ok("User registered successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);  // Email already registered
        }
    }
}