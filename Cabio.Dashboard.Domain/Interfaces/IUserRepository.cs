using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
    }
}
