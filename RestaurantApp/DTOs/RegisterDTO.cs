namespace RestaurantApp.DTOs;

public record class RegisterDTO
{

    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

}
