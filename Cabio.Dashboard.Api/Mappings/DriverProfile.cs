using AutoMapper;
using Cabio.Dashboard.Api.Dtos;
using Cabio.Dashboard.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cabio.Dashboard.API.Mappings
{
    public class DriverProfile : Profile
    {
        public DriverProfile()
        {
            CreateMap<Driver, DriverDto>();
            CreateMap<CreateDriverDto, Driver>();
        }
    }
}
