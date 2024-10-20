namespace BeverageAPI.Models;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = [];

    public void Validate()
    {
        if (string.IsNullOrEmpty(Name)) throw new Exception("Customer name is required.");
        if (string.IsNullOrEmpty(Email)) throw new Exception("Customer email is required.");
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Email: {Email}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Customer other)
        {
            return Id == other.Id && Name == other.Name && Email == other.Email;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Email);
    }
}