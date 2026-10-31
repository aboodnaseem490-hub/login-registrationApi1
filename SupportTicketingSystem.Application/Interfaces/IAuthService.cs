
using SupportTicketingSystem.Application.DTOs;

namespace SupportTicketingSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);

        Task<(bool Success, string Message, LoginResponseDto? Data)>
            LoginAsync(LoginDto dto);

        Task<(bool Success, string Message)>
            CreateSupportAsync(CreateSupportDto dto);
    }
}
