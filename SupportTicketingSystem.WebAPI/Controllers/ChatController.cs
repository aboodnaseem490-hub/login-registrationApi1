using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SupportTicketingSystem.Application.Interfaces.ChatInterfaces;
using SupportTicketingSystem.Application.DTOs.ChatDTOs;

namespace SupportTicketingSystem.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        // GET: api/Chat/5/messages

        [HttpGet("{ticketId}/messages")]
        public async Task<IActionResult> GetMessages(int ticketId)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var userType = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userType))
                return Unauthorized();

            try
            {
                var messages = await _chatService.GetMessagesAsync(
                    ticketId,
                    userId,
                    userType
                );

                return Ok(messages);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("{ticketId}/messages")]
        public async Task<IActionResult> SendMessage(
            int ticketId,
            [FromBody] SendChatMessageDto dto)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var userType = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userType))
                return Unauthorized();

            try
            {
                var message = await _chatService.SendMessageAsync(
                    ticketId,
                    userId,
                    userType,
                    dto.Message
                );

                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
