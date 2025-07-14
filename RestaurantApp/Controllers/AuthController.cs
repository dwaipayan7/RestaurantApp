using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.DTOs;
using RestaurantApp.Services;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly AuthService _authService;


        public AuthController(UserService user, AuthService auth)
        {
            _userService = user;
            _authService = auth;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var existing = await _userService.GetByEmailAsync(dto.Email);
            if (existing != null)
            {
                return BadRequest("User Already Exist");
            }

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = _userService.CreateAsync(new User { Name = dto.Name, Email = dto.Email, PasswordHash = hash });

            return Ok(new{user, Message = "Registered"});

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _userService.GetByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateToken(user.Email);
            return Ok(new { token });
        }

    }
}
