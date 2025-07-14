using System;
using MongoDB.Driver;
using RestaurantApp.Models;

namespace RestaurantApp.Services;

public class RestaurantService
{

    private readonly IMongoCollection<Restaurant> _restaurants;

    public RestaurantService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:Database"]);
        _restaurants = database.GetCollection<Restaurant>("Restaurants");
    }

    public async Task CreateAsync(Restaurant restaurant)
    {
        await _restaurants.InsertOneAsync(restaurant);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _restaurants.Find(_ => true).ToListAsync();
    }

    public async Task AddMenuItemAsync(string restaurantId, FoodItem item)
    {
        var update = Builders<Restaurant>.Update.Push(r => r.Menu, item);
        await _restaurants.UpdateOneAsync(r => r.Id == restaurantId, update);
    }

}
