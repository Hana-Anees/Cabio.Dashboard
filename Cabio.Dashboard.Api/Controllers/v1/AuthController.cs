using Cabio.Dashboard.Application.Commands.Users;
using Cabio.Dashboard.Application.Dtos.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cabio.Dashboard.Api.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // --- POST: api/Auth/signup ---
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignupDto dto)
        {
            var result = await _mediator.Send(new SignupUserCommand(dto));

            if (!result)
                return BadRequest("Username already exists.");

            return Ok("User registered successfully.");
        }

        // --- POST: api/Auth/login ---
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _mediator.Send(new LoginUserCommand(dto));

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid username or password.");

            return Ok(new { token });
        }
    }
}
