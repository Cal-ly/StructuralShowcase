namespace BeverageAPI.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly BeverageContext _context;

    public AuthService(IConfiguration configuration, BeverageContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public void RegisterUser(User newUser)
    {
        newUser.Role = UserRole.Customer;  // Assign customer role
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public void RegisterAdmin(User newUser)
    {
        newUser.Role = UserRole.Admin;  // Assign admin role
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }


    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public string Authenticate(string email, string password)
    {
        var user = _context.Users.First(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSettings = _configuration.GetSection("Jwt");
        var secret = jwtSettings["Key"];
        if (string.IsNullOrEmpty(secret))
        {
            Console.WriteLine("JWT Secret-Key is missing or null in AuthService.");
            throw new InvalidOperationException("JWT Secret is not configured.");
        }
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role?.ToString() ?? nameof(UserRole.Customer))  // Add role to token claims
            ]),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"] ?? "720")),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}