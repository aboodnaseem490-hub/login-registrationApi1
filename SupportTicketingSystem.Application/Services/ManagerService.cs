
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Data.Repositories.ManagerRepositories;
using SupportTicketingSystem.Data.Entities;
namespace SupportTicketingSystem.Application.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

public async Task<List<ManagerTicketDto>> GetAllTicketsAsync(string? status)        {
            var tickets = await _managerRepository.GetAllTicketsAsync(status);
            return tickets.Select(t => new ManagerTicketDto
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

        public async Task<(bool Success, string Message)> AssignTicketAsync(
    int managerId, AssignTicketDto dto)
        {
            var ticket = await _managerRepository
                .GetTicketByIdAsync(dto.TicketId);

            if (ticket == null)
                return (false, "Ticket not found.");

            if (ticket.Status != "New")
                return (false, "Only new tickets can be assigned.");

            var supportMember = await _managerRepository
                .GetSupportMemberAsync(dto.SupportMemberId);

            if (supportMember == null)
                return (false, "Support member not found or inactive.");

            ticket.AssignedToId = supportMember.Id;
            ticket.AssignedAt = DateTime.UtcNow;
            ticket.Status = "Assigned";

            var history = new TicketAssignmentHistory
            {
                TicketId = ticket.Id,
                SupportMemberId = supportMember.Id,
                ManagerId = managerId,
                Action = "Assigned",
                ActionAt = DateTime.UtcNow
            };

            await _managerRepository.AddAssignmentHistoryAsync(history);
            await _managerRepository.SaveChangesAsync();

            return (true, "Ticket assigned successfully.");
        }
    }
}
