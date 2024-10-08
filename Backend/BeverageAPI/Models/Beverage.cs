namespace BeverageAPI.Models;

public class Beverage
{
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }

    public void Validate()
    {
        ValidateId();
        ValidateBrand();
        ValidateDescription();
        ValidatePrice();
        ValidateSize();
    }

    private void ValidateId()
    {
        if (Id <= 0)
        {
            throw new ArgumentException("Id must be positive.");
        }
    }

    private void ValidateBrand()
    {
        if (Brand == null)
        {
            throw new ArgumentNullException("Brand cannot be null.");
        }

        if (Brand.Length < 2 || Brand.Length > 50)
        {
            throw new ArgumentException("Brand must be between 2 and 50 characters.");
        }
    }

    private void ValidateDescription()
    {
        if (Description == null)
        {
            throw new ArgumentNullException("Brand cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(Description) || Description.Length < 5)
        {
            throw new ArgumentException("Description must be at least 5 characters.");
        }
    }

    private void ValidatePrice()
    {
        if (Price < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }
    }

    private void ValidateSize()
    {
        if (!Enum.IsDefined(typeof(SizeEnum), Size))
        {
            throw new ArgumentException("Invalid size.");
        }
    }

    public override string ToString()
    {
        return $"Id: {Id}, Brand: {Brand}, Description: {Description}, Price: {Price:C}, Size: {Size}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Beverage other)
        {
            return Id == other.Id &&
                   Brand == other.Brand &&
                   Description == other.Description &&
                   Price == other.Price &&
                   Size == other.Size;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Brand, Description, Price, Size);
    }
}
