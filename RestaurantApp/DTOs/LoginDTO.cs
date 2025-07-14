namespace RestaurantApp.DTOs;

public record class LoginDTO
{

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}
