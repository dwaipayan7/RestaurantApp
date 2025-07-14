using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantApp.Models;

public class Restaurant
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public List<FoodItem> Menu { get; set; } = [];

}
