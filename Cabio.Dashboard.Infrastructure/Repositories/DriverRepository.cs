using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Domain.Interfaces;
using Cabio.Dashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cabio.Dashboard.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;

        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync() =>
            await _context.Drivers.Include(d => d.Vehicles).ToListAsync();

        public async Task<Driver?> GetByIdAsync(int id) =>
            await _context.Drivers.Include(d => d.Vehicles)
                                  .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Driver> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver != null)
            {
                _context.Drivers.Remove(driver);
                await _context.SaveChangesAsync();
            }
        }
    }
}
