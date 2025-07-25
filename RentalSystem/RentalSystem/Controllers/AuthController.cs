using Microsoft.AspNetCore.Mvc;
using RentalSystem.Application.DTOs.Authentication;
using RentalSystem.Application.Interfaces;

namespace RentalSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtService;

        public AuthController(IJwtTokenService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login.Username == "admin" && login.Password == "password")
            {
                var token = _jwtService.GenerateToken(login.Username, "Admin");
                return Ok(new AuthResponseDto(token));
            }

            if (login.Username == "user" && login.Password == "password")
            {
                var token = _jwtService.GenerateToken(login.Username, "User");
                return Ok(new AuthResponseDto(token));
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
