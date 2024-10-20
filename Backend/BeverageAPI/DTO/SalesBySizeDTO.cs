namespace BeverageAPI.DTO;

public class SalesBySizeDTO
{
    public SizeEnum Size { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalRevenue { get; set; }
}