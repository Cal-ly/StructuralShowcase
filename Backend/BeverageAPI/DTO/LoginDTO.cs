namespace BeverageAPI.DTO;

public class LoginDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class RegisterDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    //public int CustomerId { get; set; }  // Associate the user with a customer
}