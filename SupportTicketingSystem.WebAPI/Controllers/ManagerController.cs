
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Application.DTOs;
using System.Security.Claims;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        // Get all tickets with their statuses
        [HttpGet("tickets")]
        public async Task<IActionResult> GetAllTickets(
    [FromQuery] string? status)
        {
            var tickets = await _managerService.GetAllTicketsAsync(status);

            return Ok(tickets);
        }

        // Assign ticket to support member
        [HttpPost("assign-ticket")]
        public async Task<IActionResult> AssignTicket(
    [FromBody] AssignTicketDto dto)
        {
            var managerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(managerIdClaim, out int managerId))
                return Unauthorized();

            var result = await _managerService.AssignTicketAsync(managerId, dto);

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
