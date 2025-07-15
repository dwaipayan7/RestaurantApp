using System;

namespace RestaurantApp.Models;

public class PagedApiResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalRecords { get; set; }
}
