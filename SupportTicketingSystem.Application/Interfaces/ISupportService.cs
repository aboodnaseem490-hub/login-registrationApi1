using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.DTOs.SupportDTOs;


namespace SupportTicketingSystem.Application.Interfaces
{
    public interface ISupportService
    {
        Task<List<SupportTicketDto>> GetAssignedTicketsAsync(int supportMemberId);

        Task<(bool Success, string Message)> HandleTicketActionAsync(
            int ticketId,
            int supportMemberId,
            string action);
        Task<(bool Success, string Message)> ResolveTicketAsync(
    int ticketId,
    int supportMemberId);
    }
}