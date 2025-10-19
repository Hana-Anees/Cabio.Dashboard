using Cabio.Dashboard.Application.Dtos.Users;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Users
{
    public record LoginUserCommand(UserLoginDto LoginDto) : IRequest<string>;
}
