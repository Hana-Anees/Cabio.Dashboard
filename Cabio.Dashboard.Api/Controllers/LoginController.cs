using Cabio.Dashboard.Api.Models;
using Cabio.Dashboard.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cabio.Dashboard.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "password123")
            {
                var token = _jwtService.GenerateToken(request.Username, "Admin");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [HttpGet("secure-data")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult SecureData()
        {
            return Ok(new { message = "You have access to secure data!" });
        }
    }
}
