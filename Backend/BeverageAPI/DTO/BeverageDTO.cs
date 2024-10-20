namespace BeverageAPI.DTO;

public class BeverageDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public SizeEnum Size { get; set; }
}