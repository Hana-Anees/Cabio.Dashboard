using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Domain.Interfaces;
using Cabio.Dashboard.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Cabio.Dashboard.Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repository;

        public DriverService(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<Driver> CreateDriverAsync(Driver driver)
        {           
            return await _repository.AddAsync(driver);
        }
    }
}
