using AutoMapper;
using Cabio.Dashboard.Api.Models;
using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Drivers.Queries
{
    public record GetAllDriversQuery() : IRequest<IEnumerable<DriverDto>>;

    public class GetAllDriversHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<DriverDto>>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDriversHandler(IDriverRepository repository, IMapper mapper)
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
