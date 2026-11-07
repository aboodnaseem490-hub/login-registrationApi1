using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Application.DTOs.ChatDTOs;

namespace SupportTicketingSystem.Application.Interfaces.ChatInterfaces
{
    public interface IChatService
    {
        Task<List<ChatMessageDto>> GetMessagesAsync(
            int ticketId,
            int userId,
            string userType);

        Task<ChatMessageDto> SendMessageAsync(
            int ticketId,
            int senderId,
            string userType,
            string message);
    }
}