using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Data.Entities;
using SupportTicketingSystem.Data.Repositories.TicketRepositories;

namespace SupportTicketingSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<(bool Success, string Message, int? TicketId)>
            CreateTicketAsync(int clientId, CreateTicketDto dto)
        {
            var productExists =
                await _ticketRepository.ProductExistsAsync(dto.ProductId);

            if (!productExists)
            {
                return (false, "Product does not exist or is inactive.", null);
            }

            var ticket = new Ticket
            {
                ClientId = clientId,
                ProductId = dto.ProductId,
                Title = dto.Title,
                Description = dto.Description,
                Status = "New",
                CreatedAt = DateTime.UtcNow
            };

            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return (true, "Ticket created successfully.", ticket.Id);
        }
    }
}