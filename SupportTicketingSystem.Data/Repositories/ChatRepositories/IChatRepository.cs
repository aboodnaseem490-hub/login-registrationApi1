using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.ChatRepositories
{
    public interface IChatRepository
    {
        Task<Ticket?> GetTicketForChatAsync(int ticketId);
        Task<List<TicketMessage>> GetMessagesByTicketIdAsync(int ticketId);

        Task AddMessageAsync(TicketMessage message);

        Task SaveChangesAsync();
    }
}