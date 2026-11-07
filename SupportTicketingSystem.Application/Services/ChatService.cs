using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Application.DTOs.ChatDTOs;
using SupportTicketingSystem.Application.Interfaces.ChatInterfaces;
using SupportTicketingSystem.Data.Entities;
using SupportTicketingSystem.Data.Repositories.ChatRepositories;

namespace SupportTicketingSystem.Application.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(
            int ticketId,
            int userId,
            string userType)
        {
            var ticket = await _chatRepository.GetTicketForChatAsync(ticketId);

            ValidateChatAccess(ticket, userId, userType);

            var messages = await _chatRepository
                .GetMessagesByTicketIdAsync(ticketId);

            return messages.Select(m => new ChatMessageDto
            {
                Id = m.Id,
                TicketId = m.TicketId,
                SenderId = m.SenderId,
                SenderName = m.Sender.FullName,
                Message = m.Message,
                SentAt = m.SentAt
            }).ToList();
        }

        public async Task<ChatMessageDto> SendMessageAsync(
            int ticketId,
            int senderId,
            string userType,
            string message)
        {
            var ticket = await _chatRepository.GetTicketForChatAsync(ticketId);

            ValidateChatAccess(ticket, senderId, userType);

            if (ticket!.Status != "InProgress")
                throw new InvalidOperationException(
                    "Chat is only available for tickets in progress.");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty.");

            message = message.Trim();

            if (message.Length > 2000)
                throw new ArgumentException(
                    "Message cannot exceed 2000 characters.");

            var newMessage = new TicketMessage
            {
                TicketId = ticketId,
                SenderId = senderId,
                Message = message,
                SentAt = DateTime.UtcNow
            };

            await _chatRepository.AddMessageAsync(newMessage);
            await _chatRepository.SaveChangesAsync();

            return new ChatMessageDto
            {
                Id = newMessage.Id,
                TicketId = newMessage.TicketId,
                SenderId = newMessage.SenderId,
                SenderName = userType == UserTypes.Client
                    ? ticket.Client?.FullName ?? string.Empty
                    : ticket.AssignedTo?.FullName ?? string.Empty,
                Message = newMessage.Message,
                SentAt = newMessage.SentAt
            };
        }

        private static void ValidateChatAccess(
            Ticket? ticket,
            int userId,
            string userType)
        {
            if (ticket == null)
                throw new KeyNotFoundException("Ticket not found.");

            if (ticket.Status != "InProgress")
                throw new InvalidOperationException(
                    "Chat is only available for tickets in progress.");

            bool hasAccess =
                (userType == UserTypes.Client && ticket.ClientId == userId) ||
                (userType == UserTypes.Support && ticket.AssignedToId == userId);

            if (!hasAccess)
                throw new UnauthorizedAccessException(
                    "You are not allowed to access this chat.");
        }
    }
}