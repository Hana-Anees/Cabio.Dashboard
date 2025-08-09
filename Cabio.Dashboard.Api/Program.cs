using Cabio.Dashboard.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT config (later move to appsettings.json)
var jwtSecret = "SuperSecretKey1234567890987654321123456";
var jwtIssuer = "Cabio.Dashboard";

// Add JwtService
builder.Services.AddSingleton(new JwtService(jwtSecret, jwtIssuer));

// Add Authentication
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

// Add Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cabio Dashboard API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Swagger should be before Authentication for dev/testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cabio Dashboard API v1");
    c.RoutePrefix = "swagger"; // so it opens at /swagger
});

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Sample Login Endpoint
app.MapPost("/login", (string username, string password, JwtService jwtService) =>
{
    if (username == "admin" && password == "password123")
    {
        var token = jwtService.GenerateToken(username, "Admin");
        return Results.Ok(new { token });
    }
    return Results.Unauthorized();
});

// Secure Endpoint Example
app.MapGet("/secure-data", [Microsoft.AspNetCore.Authorization.Authorize] () =>
{
    return Results.Ok(new { message = "You have access to secure data!" });
});

app.Run();
