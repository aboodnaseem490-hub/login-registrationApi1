using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Context;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.SupportRepositories
{

    public class SupportRepository : ISupportRepository
    {
        public async Task<Ticket?> GetTicketByIdForSupportAsync(
        int ticketId,
        int supportMemberId)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t =>
                    t.Id == ticketId &&
                    t.AssignedToId == supportMemberId);
        }
        private readonly ApplicationDbContext _context;

        public SupportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAssignedTicketsAsync(int supportMemberId)
        {
            return await _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Product)
                .Where(t =>
                    t.AssignedToId == supportMemberId &&
                    t.Status != "New" &&
                    t.Status != "Closed")
                .OrderByDescending(t => t.AssignedAt)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }
        public async Task<TicketAssignmentHistory?> GetLatestAssignmentHistoryAsync(
    int ticketId)
        {
            return await _context.TicketAssignmentHistories
                .Where(h => h.TicketId == ticketId)
                .OrderByDescending(h => h.ActionAt)
                .FirstOrDefaultAsync();
        }

        public async Task AddAssignmentHistoryAsync(TicketAssignmentHistory history)
        {
            await _context.TicketAssignmentHistories.AddAsync(history);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}