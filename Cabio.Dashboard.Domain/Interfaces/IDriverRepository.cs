using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Domain.Interfaces
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver?> GetByIdAsync(int id);
        Task<Driver> AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(int id);
    }
}
