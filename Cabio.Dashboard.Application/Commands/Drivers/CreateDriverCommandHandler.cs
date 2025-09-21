using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Application.Services.Interfaces;
using Cabio.Dashboard.Domain.Entities;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverDto>
    {
        private readonly IDriverService _driverService;
        private readonly IMapper _mapper;

        public CreateDriverCommandHandler(IDriverService driverService, IMapper mapper)
        {
            _driverService = driverService;
            _mapper = mapper;
        }

        public async Task<DriverDto> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {           
            var driver = _mapper.Map<Driver>(request.DriverDto);           
            var createdDriver = await _driverService.CreateDriverAsync(driver);           
            return _mapper.Map<DriverDto>(createdDriver);
        }
    }
}
