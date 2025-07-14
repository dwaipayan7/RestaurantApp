using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantApp;

public class User
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

}
