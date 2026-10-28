using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.WebAPI.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? MobileNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
    }
}