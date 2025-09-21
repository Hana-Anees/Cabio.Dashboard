using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public record DeleteDriverCommand(int Id) : IRequest<bool>;
}
