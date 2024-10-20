namespace BeverageAPI.Services;

public class Validator
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }

    public void Validate()
    {
        ValidateId();
        ValidateName();
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

    private void ValidateName()
    {
        if (Name == null)
        {
            throw new ArgumentNullException("Name cannot be null.");
        }

        if (Name.Length < 2 || Name.Length > 50)
        {
            throw new ArgumentException("Name must be between 2 and 50 characters.");
        }
    }

    private void ValidateDescription()
    {
        if (Description == null)
        {
            throw new ArgumentNullException("Name cannot be null.");
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
}