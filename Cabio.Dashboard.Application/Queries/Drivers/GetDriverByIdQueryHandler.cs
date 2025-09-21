using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Queries.Drivers
{
    public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, DriverDto>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public GetDriverByIdQueryHandler(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DriverDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            var driver = await _repository.GetByIdAsync(request.Id);

            if (driver == null)
                throw new KeyNotFoundException($"Driver with ID {request.Id} not found.");

            return _mapper.Map<DriverDto>(driver);
        }
    }
}
