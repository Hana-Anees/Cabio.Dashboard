using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Entities;
using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, DriverDto>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDriverCommandHandler(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DriverDto> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Driver with ID {request.Id} not found.");

            // Map updates into the existing entity
            _mapper.Map(request.DriverDto, existing);

            await _repository.UpdateAsync(existing);

            return _mapper.Map<DriverDto>(existing);
        }
    }
}
