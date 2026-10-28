using Microsoft.EntityFrameworkCore;
using SupportTicketingSystem.Data.Context;
using SupportTicketingSystem.Data.Entities;

namespace SupportTicketingSystem.Data.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == usernameOrEmail ||
                u.Email == usernameOrEmail);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}