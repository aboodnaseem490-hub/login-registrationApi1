using System.ComponentModel.DataAnnotations;

namespace SupportTicketingSystem.Application.DTOs
{
    public class RegisterDto : IValidatableObject
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(150, ErrorMessage = "Full name cannot exceed 150 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Username must be between 3 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9_.]+$",
            ErrorMessage = "Username can contain only English letters, numbers, underscores, and dots.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address, for example name@example.com.")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^(?:\+9627|009627|07)[789]\d{7}$",
            ErrorMessage = "Enter a valid Jordanian mobile number, e.g. 0791234567 or +962791234567.")]
        public string MobileNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string? Address { get; set; }

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (DateOfBirth.HasValue &&
                DateOfBirth.Value.Date > DateTime.Today)
            {
                yield return new ValidationResult(
                    "Date of birth cannot be in the future.",
                    new[] { nameof(DateOfBirth) });
            }
        }
    }
}
