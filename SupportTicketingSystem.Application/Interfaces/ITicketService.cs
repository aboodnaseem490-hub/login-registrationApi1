using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Application.DTOs;

namespace SupportTicketingSystem.Application.Interfaces
{
    public interface ITicketService
    {

        Task<(bool Success, string Message, int? TicketId)> CreateTicketAsync(
            int clientId,
            CreateTicketDto dto);
        Task ConfirmResolutionAsync(int clientId, int ticketId);
    }
}