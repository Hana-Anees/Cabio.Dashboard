using Cabio.Dashboard.Application.Dtos.Drivers;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public record CreateDriverCommand(CreateDriverDto DriverDto) : IRequest<DriverDto>;
}
