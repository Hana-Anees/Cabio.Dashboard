using Cabio.Dashboard.Api.Dtos;
using MediatR;

namespace Cabio.Dashboard.Application.Drivers.Commands.CreateDriver
{
    public record CreateDriverCommand(CreateDriverDto DriverDto) : IRequest<DriverDto>;
}
