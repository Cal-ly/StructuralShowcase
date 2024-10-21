namespace BeverageAPI.DTO;

public class LoginDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterDTO
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    //public int CustomerId { get; set; }  // Associate the user with a customer
}