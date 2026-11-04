using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketingSystem.Data.Entities
{
    public class TicketMessage
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int SenderId { get; set; }

        public string Message { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public Ticket Ticket { get; set; } = null!;

        public User Sender { get; set; } = null!;
    }
}