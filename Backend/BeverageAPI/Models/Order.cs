using BeverageAPI.Models.Enums;

namespace BeverageAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public StatusEnum? Status { get; set; } = StatusEnum.Pending;
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = new();

    public void CalculateTotalAmount()
    {
        TotalAmount = 0;
        if (OrderItems?.Count > 0)
        {
            foreach (var item in OrderItems)
            {
                if (item.Price > 0)
                {
                    item.CalculatePrice();
                    TotalAmount += item.Price;
                }
            }
        }
    }

    public void Validate()
    {
        if (TotalAmount < 0)
        {
            throw new Exception("Total amount must be zero or greater.");
        }
        if (OrderItems == null || OrderItems.Count == 0)
        {
            throw new Exception("Order must contain at least one item.");
        }
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
