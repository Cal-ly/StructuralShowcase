namespace BeverageAPI.Services;

public class AdminService
{
    private readonly BeverageContext _context;

    public AdminService(BeverageContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();  // Return all users
    }

    public User GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found.");

        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public void ResetPassword(int userId, string newPassword)
    {
        var user = _context.Users.Find(userId);
        if (user == null) throw new KeyNotFoundException("User not found.");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        _context.SaveChanges();
    }

    public void UpdateUserEmail(int userId, string newEmail)
    {
        var user = _context.Users.Find(userId);
        if (user == null) throw new KeyNotFoundException("User not found.");

        // Check if the new email is already in use
        if (_context.Users.Any(u => u.Email == newEmail))
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        user.Email = newEmail;
        _context.SaveChanges();
    }
}