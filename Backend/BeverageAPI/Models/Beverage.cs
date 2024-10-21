using BeverageAPI.Models.Enums;

namespace BeverageAPI.Models;

public class Beverage
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }

    public void Validate()
    {
        if (Price < 0) throw new Exception("Price must be zero or greater.");
        if (Price > 10000) throw new Exception("Price exceeds maximum allowed limit.");
        if (string.IsNullOrEmpty(Name)) throw new Exception("Name is required.");
        if (!Enum.IsDefined(typeof(SizeEnum), Size)) throw new Exception("Invalid size value provided.");
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Description: {Description}, Price: {Price:C}, Size: {Size}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Beverage other)
        {
            return Id == other.Id &&
                   Name == other.Name &&
                   Description == other.Description &&
                   Price == other.Price &&
                   Size == other.Size;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, Price, Size);
    }
}