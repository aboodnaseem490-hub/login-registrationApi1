using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.Application.DTOs.ChatDTOs
{
    public class SendChatMessageDto
    {
        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string Message { get; set; } = string.Empty;
    }
}