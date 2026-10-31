
using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.Application.DTOs
{
    public class CreateSupportDto
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?:\+9627|009627|07)[789]\d{7}$",
            ErrorMessage = "Enter a valid Jordanian mobile number.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
    }
}
