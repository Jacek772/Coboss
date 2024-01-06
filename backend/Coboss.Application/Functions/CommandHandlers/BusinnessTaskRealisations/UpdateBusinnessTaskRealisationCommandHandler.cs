using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskRealisations
{
    public class UpdateBusinnessTaskRealisationCommandHandler : IRequestHandler<UpdateBusinnessTaskRealisationCommand, Unit>
    {
        private readonly IBusinnessTaskRealisationsService _businnessTaskRealisationsService;

        public UpdateBusinnessTaskRealisationCommandHandler(IBusinnessTaskRealisationsService businnessTaskRealisationsService)
        {
            _businnessTaskRealisationsService = businnessTaskRealisationsService;
        }

        public async Task<Unit> Handle(UpdateBusinnessTaskRealisationCommand request, CancellationToken cancellationToken)
        {
            await _businnessTaskRealisationsService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}
