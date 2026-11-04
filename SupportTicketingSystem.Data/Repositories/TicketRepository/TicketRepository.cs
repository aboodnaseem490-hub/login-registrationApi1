using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Context;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.TicketRepositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Products
                .AnyAsync(p => p.Id == productId && p.IsActive);
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}