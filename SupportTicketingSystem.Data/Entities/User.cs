using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string UserType { get; set; } = "Client";

        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}