using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Entities;

namespace Cabio.Dashboard.Application.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            // Domain → DTOs
            CreateMap<Driver, DriverDto>();

            // DTOs → Domain
            CreateMap<CreateDriverDto, Driver>();
            CreateMap<UpdateDriverDto, Driver>();
        }
    }
}
