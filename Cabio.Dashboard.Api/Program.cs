using Cabio.Dashboard.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration values
var jwtSecret = "SuperSecretKey123456789"; // Move to appsettings.json later
var jwtIssuer = "Cabio.Dashboard";

// Add JwtService to DI
builder.Services.AddSingleton(new JwtService(jwtSecret, jwtIssuer));

// Configure JWT Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", (string username, string password, JwtService jwtService) =>
{
    // Hardcoded users for now
    if (username == "admin" && password == "password123")
    {
        var token = jwtService.GenerateToken(username, "Admin");
        return Results.Ok(new { token });
    }
    return Results.Unauthorized();
});

app.MapGet("/secure-data", [Microsoft.AspNetCore.Authorization.Authorize] () =>
{
    return Results.Ok(new { message = "You have access to secure data!" });
});

app.Run();
