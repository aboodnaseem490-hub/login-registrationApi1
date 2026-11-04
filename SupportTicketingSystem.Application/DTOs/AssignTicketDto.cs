using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.Application.DTOs
{
    public class AssignTicketDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TicketId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SupportMemberId { get; set; }
    }
}