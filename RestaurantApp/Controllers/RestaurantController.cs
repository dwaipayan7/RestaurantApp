using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        public async Task<ApiResponse<Restaurant>> Create(RestaurantDTO dto)
        {

            var res = new ApiResponse<Restaurant>();
            try
            {
                var restaurant = new Restaurant
                {
                    Name = dto.Name,
                    Location = dto.Location,
                    Menu = new List<FoodItem>()
                };
                await _service.CreateAsync(restaurant);
                res.Status = true;
                res.Message = "Created Successfully";
                res.Result = restaurant;
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.Message;
            }

            return res;
        }

        [HttpPost("add-food")]
        public async Task<ApiResponse<FoodItem>> AddFood(FoodItemDTO dto)
        {

            var res = new ApiResponse<FoodItem>();

            try
            {

                var item = new FoodItem
                {
                    Name = dto.Name,
                    Price = dto.Price
                };
                var result = _service.AddMenuItemAsync(dto.RestaurantId, item);

                res.Status = true;
                res.Message = "Food Added";

            }
            catch (Exception e)
            {


                res.Status = false;
                res.Message = e.ToString();
            }
            return res;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Restaurant>>> GetAll()
        {
            var res = new ApiResponse<List<Restaurant>>();

            try
            {
                var data = await _service.GetAllAsync();
                res.Status = true;
                res.Result = data!;
                res.Message = "Data Fetched";
            }
            catch (Exception e)
            {
                res.Status = false;
                res.Message = e.ToString();

            }
            return res;
        }

        [HttpGet("paged")]
        public async Task<PagedApiResponse<Restaurant>> GetPaged(
                   [FromQuery] int page = 1,
                   [FromQuery] int limit = 10)
        {
            var res = new PagedApiResponse<Restaurant>();
            try
            {
                // Get all restaurants (consider implementing true paging in service)
                var allRestaurants = await _service.GetAllAsync();

                // Calculate pagination
                var totalCount = allRestaurants.Count;
                var pagedItems = allRestaurants
                    .Skip((page - 1) * limit)
                    .Take(limit)
                    .ToList();

                // Set response values
                res.Items = pagedItems;
                res.TotalRecords = totalCount;
            }
            catch (Exception e)
            {
                // Consider adding error handling to PagedApiResponse
                // For now, return empty results with 0 count
                res.Items = new List<Restaurant>();
                res.TotalRecords = 0;
                // Log the exception here
            }
            return res;
        }



    }
}
