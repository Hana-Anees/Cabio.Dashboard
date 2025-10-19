using Cabio.Dashboard.Application.Dtos.Users;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Users
{
    public record SignupUserCommand(UserSignupDto SignupDto) : IRequest<bool>;
}
