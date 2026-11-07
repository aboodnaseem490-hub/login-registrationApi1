
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using SupportTicketingSystem.Application.Interfaces.ChatInterfaces;

namespace SupportTicketingSystem.WebAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task JoinTicketChat(int ticketId)
        {
            var userId = GetUserId();
            var userType = GetUserType();

            // Verify that the user can access this ticket's chat
            await _chatService.GetMessagesAsync(
                ticketId,
                userId,
                userType
            );

            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                GetGroupName(ticketId)
            );
        }

        public async Task SendMessage(int ticketId, string message)
        {
            var userId = GetUserId();
            var userType = GetUserType();

            var savedMessage = await _chatService.SendMessageAsync(
                ticketId,
                userId,
                userType,
                message
            );

            await Clients.Group(GetGroupName(ticketId))
                .SendAsync("ReceiveMessage", savedMessage);
        }

        private int GetUserId()
        {
            var userId = Context.User?.FindFirst(
                ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out var id))
                throw new HubException("Invalid user identity.");

            return id;
        }

        private string GetUserType()
        {
            var userType = Context.User?.FindFirst(
                ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userType))
                throw new HubException("User role not found.");

            return userType;
        }

        private static string GetGroupName(int ticketId)
        {
            return $"ticket-{ticketId}";
        }
    }
}
