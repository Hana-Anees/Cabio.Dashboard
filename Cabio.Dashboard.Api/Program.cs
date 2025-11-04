using Cabio.Dashboard.Api.Middleware;
using Cabio.Dashboard.Application.Queries.Drivers;
using Cabio.Dashboard.Application.Services;
using Cabio.Dashboard.Application.Validators;
using Cabio.Dashboard.Application.Validators.Drivers;
using Cabio.Dashboard.Auth.Services;
using Cabio.Dashboard.Domain.Interfaces;
using Cabio.Dashboard.Infrastructure;
using Cabio.Dashboard.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Database & Infrastructure ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString);

// --- JWT ---
var jwtSecret = "SuperSecretKey1234567890987654321123456";
var jwtIssuer = "Cabio.Dashboard";
builder.Services.AddSingleton<IJwtService>(new JwtService(jwtSecret, jwtIssuer));

// --- Repositories & Services ---
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// --- MediatR ---
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(GetAllDriversQueryHandler).Assembly));

// --- Authentication ---
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

// --- FluentValidation ---
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDriverDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserSignupDtoValidator>();

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(Cabio.Dashboard.Application.Mappings.DriverProfile).Assembly);
builder.Services.AddAutoMapper(typeof(Cabio.Dashboard.Application.Mappings.UserProfile).Assembly);

// --- Authorization ---
builder.Services.AddAuthorization();

// --- Controllers ---
builder.Services.AddControllers();

// --- Swagger with JWT ---
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

// --- Middleware ---
app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>(); 

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cabio Dashboard API v1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

// --- Map Controllers ---
app.MapControllers();

app.Run();
