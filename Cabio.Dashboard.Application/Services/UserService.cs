using Cabio.Dashboard.Application.Dtos.Users;
using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Domain.Interfaces;
using Cabio.Dashboard.Auth.Services;
using System.Security.Cryptography;
using System.Text;

namespace Cabio.Dashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<bool> SignupAsync(UserSignupDto dto)
        {
            var existing = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return false;

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = HashPassword(dto.Password),
                Role = "User"
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user.Username, user.Role);
        }

        // --- helpers ---
        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
