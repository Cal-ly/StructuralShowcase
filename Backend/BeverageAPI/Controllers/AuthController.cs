[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO LoginDTO)
    {
        try
        {
            var token = _authService.Authenticate(LoginDTO.Email, LoginDTO.Password);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials.");
        }
    }

    // Example of a registration endpoint
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDTO RegisterDTO)
    {
        var hashedPassword = _authService.HashPassword(RegisterDTO.Password);
        var newUser = new User
        {
            Email = RegisterDTO.Email,
            PasswordHash = hashedPassword,
            // CustomerId will need to be set based on registration flow
        };

        // Save new user to the database
        _authService.RegisterUser(newUser);

        return Ok("User registered successfully.");
    }
}
