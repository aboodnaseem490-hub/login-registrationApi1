using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketingSystem.Application.DTOs
{
    public class ManagerTicketDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string? ProductImageUrl { get; set; }
    }
}