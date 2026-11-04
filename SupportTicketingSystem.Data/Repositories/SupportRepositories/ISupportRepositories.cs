using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.SupportRepositories
{
    public interface ISupportRepository
    {
        Task<Ticket?> GetTicketByIdForSupportAsync(int ticketId, int supportMemberId);
        Task<List<Ticket>> GetAssignedTicketsAsync(int supportMemberId);

        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task<TicketAssignmentHistory?> GetLatestAssignmentHistoryAsync(int ticketId);
        Task AddAssignmentHistoryAsync(TicketAssignmentHistory history);
        Task SaveChangesAsync();
    }
}