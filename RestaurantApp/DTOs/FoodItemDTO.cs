namespace RestaurantApp.DTOs;

public record class FoodItemDTO
{

    public string RestaurantId { get; set; } = string.Empty;
    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

}
