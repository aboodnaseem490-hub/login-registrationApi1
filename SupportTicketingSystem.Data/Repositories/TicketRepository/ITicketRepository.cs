using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.TicketRepositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(int ticketId);
        Task<bool> ProductExistsAsync(int productId);

        Task AddAsync(Ticket ticket);

        Task SaveChangesAsync();

    }
}