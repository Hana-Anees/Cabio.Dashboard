using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Application.Services
{
    public interface IDriverService
    {
        Task<Driver> CreateDriverAsync(Driver driver);
    }
}
