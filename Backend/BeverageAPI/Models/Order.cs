namespace BeverageAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = "Pending";  // Default status
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = new();

    public void Validate()
    {
        if (TotalAmount < 0) throw new Exception("Total amount must be zero or greater.");
        if (OrderItems == null || OrderItems.Count == 0) throw new Exception("Order must contain at least one item.");
    }

    public override string ToString()
    {
        return $"Id: {Id}, OrderDate: {OrderDate}, Status: {Status}, TotalAmount: {TotalAmount:C}, CustomerId: {CustomerId}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Order other)
        {
            return Id == other.Id && OrderDate == other.OrderDate && Status == other.Status && TotalAmount == other.TotalAmount && CustomerId == other.CustomerId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, OrderDate, Status, TotalAmount, CustomerId);
    }
}