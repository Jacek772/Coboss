using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskRealisations
{
    internal class DeleteBusinnessTaskRealisationsCommandHandler : IRequestHandler<DeleteBusinnessTaskRealisationsCommand, Unit>
    {
        private readonly IBusinnessTaskRealisationsService _businnessTaskRealisationsService;

        public DeleteBusinnessTaskRealisationsCommandHandler(IBusinnessTaskRealisationsService businnessTaskRealisationsService)
        {
            _businnessTaskRealisationsService = businnessTaskRealisationsService;
        }

        public async Task<Unit> Handle(DeleteBusinnessTaskRealisationsCommand request, CancellationToken cancellationToken)
        {
            await _businnessTaskRealisationsService.DeleteAsync(request.Ids);
            return Unit.Value;
        }
    }
}
