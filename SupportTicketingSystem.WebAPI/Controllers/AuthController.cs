using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                return Unauthorized(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message,
                user = result.Data
            });
        }
    }
}