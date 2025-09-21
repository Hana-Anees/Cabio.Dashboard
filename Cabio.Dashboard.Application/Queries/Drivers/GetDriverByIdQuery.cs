using Cabio.Dashboard.Application.Dtos.Drivers;
using MediatR;

namespace Cabio.Dashboard.Application.Queries.Drivers
{
    public record GetDriverByIdQuery(int Id) : IRequest<DriverDto>;
}
