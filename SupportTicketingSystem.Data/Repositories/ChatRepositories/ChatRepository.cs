using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Context;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.ChatRepositories
{
    public class ChatRepository : IChatRepository
    {
        public async Task<Ticket?> GetTicketForChatAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketMessage>> GetMessagesByTicketIdAsync(
            int ticketId)
        {
            return await _context.TicketMessages
                .Include(m => m.Sender)
                .Where(m => m.TicketId == ticketId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public async Task AddMessageAsync(TicketMessage message)
        {
            await _context.TicketMessages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}