using AutoMapper;
using Cabio.Dashboard.Application.Dtos.Drivers;
using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Drivers.Queries.GetAllDrivers
{
    // Handles the logic for GetAllDriversQuery
    public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<DriverDto>>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDriversQueryHandler(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DriverDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var drivers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }
    }
}
