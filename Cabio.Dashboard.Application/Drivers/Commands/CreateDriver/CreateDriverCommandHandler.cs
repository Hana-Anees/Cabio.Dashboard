using AutoMapper;
using Cabio.Dashboard.Api.Dtos;
using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Drivers.Commands.CreateDriver
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
