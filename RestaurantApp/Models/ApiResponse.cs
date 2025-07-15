// Keep only ONE PagedApiResponse definition
namespace RestaurantApp.Models;

public class ApiResponse<T>
{
    public bool Status { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public T? Result { get; set; }
}

