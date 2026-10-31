
namespace SupportTicketingSystem.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string UserType { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
