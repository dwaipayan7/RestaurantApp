using System;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using RestaurantApp.Models;
using MongoDB.Bson; 

namespace RestaurantApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IConfiguration config)
        {
            try
            {
                var client = new MongoClient(config["MongoDB:ConnectionString"]);
                var database = client.GetDatabase(config["MongoDB:Database"]);

                
                var command = new BsonDocument("ping", 1);
                database.RunCommand<BsonDocument>(command);
                Console.WriteLine("MongoDB connection established successfully.");

                _users = database.GetCollection<User>("Users");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MongoDB connection failed: {ex.Message}");
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

    }
}
