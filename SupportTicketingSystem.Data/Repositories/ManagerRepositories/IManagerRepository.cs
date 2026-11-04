using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.ManagerRepositories
{
    public interface IManagerRepository
    {
        Task<List<Ticket>> GetAllTicketsAsync(string? status);

        Task<Ticket?> GetTicketByIdAsync(int ticketId);

        Task<User?> GetSupportMemberAsync(int supportMemberId);

        Task AddAssignmentHistoryAsync(TicketAssignmentHistory history);

        Task SaveChangesAsync();
    }
}