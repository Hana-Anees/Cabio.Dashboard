using Cabio.Dashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
