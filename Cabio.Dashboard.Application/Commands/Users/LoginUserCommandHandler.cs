using Cabio.Dashboard.Application.Commands.Users;
using Cabio.Dashboard.Application.Services;
using MediatR;

namespace Cabio.Dashboard.Application.Handlers.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {            
            var token = await _userService.LoginAsync(request.LoginDto);

            if (token == null)
                throw new UnauthorizedAccessException("Invalid username or password");

            return token;
        }
    }
}
