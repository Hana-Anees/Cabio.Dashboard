using AutoMapper;
using Cabio.Dashboard.Domain.Interfaces;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Entities;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{    
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverDto>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public CreateDriverCommandHandler(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DriverDto> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = _mapper.Map<Driver>(request.DriverDto);
            var created = await _repository.AddAsync(driver);
            return _mapper.Map<DriverDto>(created);
        }
    }
}
