using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;
using System.Security.Claims;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Client")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(
            [FromBody] CreateTicketDto dto)
        {
            var userIdClaim = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int clientId))
            {
                return Unauthorized();
            }

            var result = await _ticketService.CreateTicketAsync(
                clientId, dto);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message,
                ticketId = result.TicketId
            });
        }
        [HttpPost("{ticketId}/confirm-resolution")]
        public async Task<IActionResult> ConfirmResolution(int ticketId)
        {
            var userIdClaim = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int clientId))
            {
                return Unauthorized();
            }

            await _ticketService.ConfirmResolutionAsync(
                clientId, ticketId);

            return Ok(new
            {
                message = "Ticket closed successfully."
            });
        }
    }
}