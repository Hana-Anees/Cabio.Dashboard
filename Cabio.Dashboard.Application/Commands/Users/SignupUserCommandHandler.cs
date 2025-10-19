using AutoMapper;
using Cabio.Dashboard.Application.Services;
using Cabio.Dashboard.Domain.Entities;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Users
{
    public class SignupUserCommandHandler : IRequestHandler<SignupUserCommand, bool>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SignupUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SignupUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request.SignupDto);
            return await _userService.SignupAsync(request.SignupDto);
        }
    }

}
