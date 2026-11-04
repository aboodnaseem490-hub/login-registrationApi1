using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Data.Entities;

using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.DTOs.SupportDTOs;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Data.Repositories.SupportRepositories;

namespace SupportTicketingSystem.Application.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;

        public SupportService(ISupportRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        public async Task<List<SupportTicketDto>> GetAssignedTicketsAsync(
            int supportMemberId)
        {
            var tickets = await _supportRepository
                .GetAssignedTicketsAsync(supportMemberId);

            return tickets.Select(t => new SupportTicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                ClientName = t.Client.FullName,
                ProductName = t.Product.Name,
                ProductImageUrl = t.Product.ImageUrl
            }).ToList();
        }

        public async Task<(bool Success, string Message)> HandleTicketActionAsync(
    int ticketId,
    int supportMemberId,
    string action)
        {
            var ticket = await _supportRepository
                .GetTicketByIdForSupportAsync(ticketId, supportMemberId);

            if (ticket == null)
                return (false, "Ticket not found or not assigned to you.");

            if (ticket.Status != "Assigned")
                return (false, "Only assigned tickets can be accepted or rejected.");

            if (action.Equals("Accept", StringComparison.OrdinalIgnoreCase))
            {
                var history = await _supportRepository
                    .GetLatestAssignmentHistoryAsync(ticketId);

                if (history == null)
                    return (false, "Assignment history not found.");

                var acceptedHistory = new TicketAssignmentHistory
                {
                    TicketId = ticket.Id,
                    SupportMemberId = supportMemberId,
                    ManagerId = history.ManagerId,
                    Action = "Accepted",
                    ActionAt = DateTime.UtcNow
                };

                ticket.Status = "InProgress";
                ticket.AcceptedAt = DateTime.UtcNow;

                await _supportRepository.AddAssignmentHistoryAsync(acceptedHistory);
                await _supportRepository.SaveChangesAsync();

                return (true, "Ticket accepted successfully.");
            }

            if (action.Equals("Reject", StringComparison.OrdinalIgnoreCase))
            {
                var history = await _supportRepository
                    .GetLatestAssignmentHistoryAsync(ticketId);

                if (history == null)
                    return (false, "Assignment history not found.");

                var rejectedHistory = new TicketAssignmentHistory
                {
                    TicketId = ticket.Id,
                    SupportMemberId = supportMemberId,
                    ManagerId = history.ManagerId,
                    Action = "Rejected",
                    ActionAt = DateTime.UtcNow
                };

                ticket.AssignedToId = null;
                ticket.AssignedAt = null;
                ticket.AcceptedAt = null;
                ticket.Status = "New";

                await _supportRepository.AddAssignmentHistoryAsync(rejectedHistory);
                await _supportRepository.SaveChangesAsync();

                return (true, "Ticket returned to manager for reassignment.");
            }

            return (false, "Invalid action. Use Accept or Reject.");
        }

    }
}