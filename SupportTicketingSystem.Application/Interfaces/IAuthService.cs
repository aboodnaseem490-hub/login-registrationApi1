using SupportTicketingSystem.Application.DTOs;

namespace SupportTicketingSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);

        Task<(bool Success, string Message, object? Data)> LoginAsync(LoginDto dto);
    }
}