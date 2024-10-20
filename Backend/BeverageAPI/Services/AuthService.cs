namespace BeverageAPI.Services;

public class AuthService
{
    private readonly BeverageContext _context;
    private readonly TokenService _tokenService;

    public AuthService(BeverageContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public string Login(string email, string password)
    {
        // Find the user by email
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Generate JWT token
        return _tokenService.GenerateToken(user);
    }

    public void Register(string email, string password, int customerId)
    {
        // Check if user already exists
        if (_context.Users.Any(u => u.Email == email))
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        // Create a new user with hashed password
        var user = new User
        {
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            CustomerId = customerId
        };

        // Add and save the user
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User GetUserById(int userId)
    {
        var user = _context.Users.Find(userId);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }
}