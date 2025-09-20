using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Application.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>();
            CreateMap<Driver, CreateDriverDto>();
        }
    }
}
