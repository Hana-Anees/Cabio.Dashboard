using Cabio.Dashboard.Domain.Interfaces;
using MediatR;

namespace Cabio.Dashboard.Application.Commands.Drivers
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, bool>
    {
        private readonly IDriverRepository _repository;

        public DeleteDriverCommandHandler(IDriverRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Driver with ID {request.Id} not found.");

            await _repository.DeleteAsync(existing.Id);
            return true;
        }
    }
}
