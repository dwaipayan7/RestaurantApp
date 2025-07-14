using System.Text;
using Microsoft.IdentityModel.Tokens;
using RestaurantApp.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services BEFORE calling builder.Build()
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<RestaurantService>();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// ✅ Add MVC support (needed for controllers)
builder.Services.AddControllers();

// ✅ Now build the app AFTER services are registered
var app = builder.Build();

// ✅ Middleware pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
