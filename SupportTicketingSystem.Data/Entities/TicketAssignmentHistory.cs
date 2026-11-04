using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketingSystem.Data.Entities
{
    public class TicketAssignmentHistory
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int SupportMemberId { get; set; }

        public int ManagerId { get; set; }

        public string Action { get; set; } = string.Empty;

        public DateTime ActionAt { get; set; }

        public Ticket Ticket { get; set; } = null!;

        public User SupportMember { get; set; } = null!;

        public User Manager { get; set; } = null!;
    }
}