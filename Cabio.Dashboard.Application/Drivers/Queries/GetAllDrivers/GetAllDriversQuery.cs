using Cabio.Dashboard.Application.Dtos.Drivers;
using MediatR;

namespace Cabio.Dashboard.Application.Drivers.Queries.GetAllDrivers
{    
    public record GetAllDriversQuery() : IRequest<IEnumerable<DriverDto>>;
}
