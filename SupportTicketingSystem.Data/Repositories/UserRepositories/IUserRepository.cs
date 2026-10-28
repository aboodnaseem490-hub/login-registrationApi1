using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);

        Task<bool> UsernameExistsAsync(string username);

        Task<bool> EmailExistsAsync(string email);

        Task AddAsync(User user);

        Task SaveChangesAsync();
    }
}