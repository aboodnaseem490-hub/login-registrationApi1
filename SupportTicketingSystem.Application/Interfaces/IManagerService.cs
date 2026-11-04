
using SupportTicketingSystem.Application.DTOs;

namespace SupportTicketingSystem.Application.Interfaces
{
    public interface IManagerService
    {
        Task<List<ManagerTicketDto>> GetAllTicketsAsync(string? status);
        Task<(bool Success, string Message)> AssignTicketAsync(
    int managerId, AssignTicketDto dto);
    }
}
