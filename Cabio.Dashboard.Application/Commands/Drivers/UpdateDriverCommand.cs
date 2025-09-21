using Cabio.Dashboard.Application.Dtos.Drivers;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public record UpdateDriverCommand(int Id, UpdateDriverDto DriverDto) : IRequest<DriverDto>;
}
