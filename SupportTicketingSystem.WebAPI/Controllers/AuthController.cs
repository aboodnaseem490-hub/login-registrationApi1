using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace SupportTicketingSystem.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtService _jwtService;

        public AuthController(IAuthService authService, JwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
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

            if (result.Data == null)
            {
                return Unauthorized(new
                {
                    message = "Login failed."
                });
            }

            result.Data.Token = _jwtService.GenerateToken(result.Data);

            return Ok(result.Data);
        }


        [Authorize(Roles = "Manager")]
        [HttpPost("create-support")]
        public async Task<IActionResult> CreateSupport(CreateSupportDto dto)
        {
            var result = await _authService.CreateSupportAsync(dto);

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

    }
}