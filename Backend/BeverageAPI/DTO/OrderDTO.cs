namespace BeverageAPI.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public decimal TotalAmount { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; } = [];
}

public class OrderItemDTO
{
    public int BeverageId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}