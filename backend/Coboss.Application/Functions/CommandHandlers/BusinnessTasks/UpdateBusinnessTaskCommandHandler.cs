using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTasks
{
    public class UpdateBusinnessTaskCommandHandler : IRequestHandler<UpdateBusinnessTaskCommand, Unit>
    {
        private readonly IBusinnessTasksService _businnessTasksService;

        public UpdateBusinnessTaskCommandHandler(IBusinnessTasksService businnessTasksService)
        {
            _businnessTasksService = businnessTasksService;
        }

        public async Task<Unit> Handle(UpdateBusinnessTaskCommand request, CancellationToken cancellationToken)
        {
            await _businnessTasksService.UpdateBusinnessTaskAsync(request);
            return Unit.Value;
        }
    }
}
