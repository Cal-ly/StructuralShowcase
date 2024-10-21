namespace BeverageAPI.Models;

public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.Customer;

    public void Validate()
    {
        if (string.IsNullOrEmpty(Email)) throw new Exception("User email is required.");
        if (string.IsNullOrEmpty(PasswordHash)) throw new Exception("Password hash is required.");
    }

    public override string ToString()
    {
        return $"Id: {Id}, Email: {Email}, CustomerId: {CustomerId}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is User other)
        {
            return Id == other.Id && Email == other.Email && PasswordHash == other.PasswordHash && CustomerId == other.CustomerId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Email, PasswordHash, CustomerId);
    }
}