namespace BeverageAPI.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int BeverageId { get; set; }
    public Beverage Beverage { get; set; } = null!;

    public void Validate()
    {
        if (Quantity <= 0) throw new Exception("Quantity must be greater than zero.");
        if (Price < 0) throw new Exception("Price must be zero or greater.");
    }

    public override string ToString()
    {
        return $"Id: {Id}, Quantity: {Quantity}, Price: {Price:C}, BeverageId: {BeverageId}, OrderId: {OrderId}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is OrderItem other)
        {
            return Id == other.Id && Quantity == other.Quantity && Price == other.Price && BeverageId == other.BeverageId && OrderId == other.OrderId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Quantity, Price, BeverageId, OrderId);
    }
}