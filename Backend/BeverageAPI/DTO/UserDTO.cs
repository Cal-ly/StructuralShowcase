namespace BeverageAPI.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public int CustomerId { get; set; }
    public CustomerDTO? Customer { get; set; }  // Optional nested CustomerDTO
}