using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.DTOs;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {

        private readonly RestaurantService _service;
        public RestaurantController(RestaurantService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create(RestaurantDTO dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Location = dto.Location,
                Menu = new List<FoodItem>()
            };

            var createdRestaurant =  _service.CreateAsync(restaurant);
            return Ok(new { Message = "Restaurant Added", createdRestaurant });
        }

        [HttpPost("add-food")]
        public async Task<IActionResult> AddFood(FoodItemDTO dto)
        {
            var item = new FoodItem
            {
                Name = dto.Name,
                Price = dto.Price
            };
            await _service.AddMenuItemAsync(dto.RestaurantId, item);
            return Ok(new { Message = "Food Item Added" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

    }
}
