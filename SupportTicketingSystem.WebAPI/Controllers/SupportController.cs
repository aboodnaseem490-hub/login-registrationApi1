using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.DTOs.SupportDTOs;
using SupportTicketingSystem.Application.Interfaces;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Support")]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }
        [HttpGet("my-tickets")]
        public async Task<IActionResult> GetMyTickets()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int supportMemberId))
                return Unauthorized();

            var tickets = await _supportService.GetAssignedTicketsAsync(supportMemberId);

            return Ok(tickets);
        }

        [HttpPost("ticket-action")]
        public async Task<IActionResult> HandleTicketAction(
        [FromBody] TicketActionDto dto
            )
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int supportMemberId))
                return Unauthorized();

            if (dto.Id <= 0 || string.IsNullOrWhiteSpace(dto.Action))
                return BadRequest(new { message = "Invalid request." });

            var result = await _supportService.HandleTicketActionAsync(
                dto.Id,
                supportMemberId,
                dto.Action);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }
}
