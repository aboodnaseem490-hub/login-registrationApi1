using Microsoft.AspNetCore.Identity;
using SupportTicketingSystem.Application.DTOs;
using SupportTicketingSystem.Application.Interfaces;
using SupportTicketingSystem.Data.Entities;
using SupportTicketingSystem.Data.Repositories.UserRepositories;


namespace SupportTicketingSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto)
        {
            if (!dto.DateOfBirth.HasValue)
            {
                return (false, "Date of birth is required.");
            }

            if (dto.DateOfBirth.Value.Date > DateTime.Today)
            {
                return (false, "Date of birth cannot be in the future.");
            }

            if (await _userRepository.UsernameExistsAsync(dto.Username))
            {
                return (false, "Username already exists.");
            }

            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                return (false, "Email already exists.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Username = dto.Username,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                UserType = UserTypes.Client,
                IsActive = true,
                DateOfBirth = dto.DateOfBirth.Value,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                dto.Password
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, "Registration successful.");
        }

        public async Task<(bool Success, string Message)> CreateSupportAsync(CreateSupportDto dto)
        {
            if (!dto.DateOfBirth.HasValue)
            {
                return (false, "Date of birth is required.");
            }

            if (dto.DateOfBirth.Value.Date > DateTime.Today)
            {
                return (false, "Date of birth cannot be in the future.");
            }

            if (await _userRepository.UsernameExistsAsync(dto.Username))
            {
                return (false, "Username already exists.");
            }

            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                return (false, "Email already exists.");
            }

            var user = new User
            {
                FullName = dto.FullName,
                Username = dto.Username,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                UserType = UserTypes.Support,
                IsActive = true,
                DateOfBirth = dto.DateOfBirth.Value,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                dto.Password
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return (true, "Support employee created successfully.");
        }



        public async Task<(bool Success, string Message, LoginResponseDto? Data)>
            LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameOrEmailAsync(
                dto.UsernameOrEmail);

            if (user == null)
            {
                return (false, "Invalid username/email or password.", null);
            }

            if (!user.IsActive)
            {
                return (false, "This account is inactive.", null);
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return (false, "Invalid username/email or password.", null);
            }

            var response = new LoginResponseDto
            {
                Message = "Login successful.",
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                UserType = user.UserType
            };

            return (true, "Login successful.", response);
        }

    }
}
