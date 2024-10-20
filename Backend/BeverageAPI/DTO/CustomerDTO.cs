namespace BeverageAPI.DTO;

public class CustomerDTO
{
    public int Id { get; set; }  // Customer ID, needed for updates and fetches
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<OrderDTO>? Orders { get; set; }
}