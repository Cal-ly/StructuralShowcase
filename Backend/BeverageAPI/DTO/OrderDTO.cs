namespace BeverageAPI.DTO;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public decimal TotalAmount { get; set; }
}