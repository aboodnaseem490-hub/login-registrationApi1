
using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Context;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.ManagerRepositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync(string? status)
        {
            var query = _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status == status);
            }

            return await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<User?> GetSupportMemberAsync(int supportMemberId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Id == supportMemberId &&
                    u.UserType == UserTypes.Support &&
                    u.IsActive);
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
