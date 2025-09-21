using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Application.Services.Interfaces
{
    public interface IDriverService
    {
        Task<Driver> CreateDriverAsync(Driver driver);
    }
}
