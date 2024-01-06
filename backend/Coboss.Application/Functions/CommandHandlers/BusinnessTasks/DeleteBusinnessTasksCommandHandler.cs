using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTasks
{
    public class DeleteBusinnessTasksCommandHandler : IRequestHandler<DeleteBusinnessTasksCommand, Unit>
    {
        private readonly IBusinnessTasksService _businnessTasksService;

        public DeleteBusinnessTasksCommandHandler(IBusinnessTasksService businnessTasksService)
        {
            _businnessTasksService = businnessTasksService;
        }

        public async Task<Unit> Handle(DeleteBusinnessTasksCommand request, CancellationToken cancellationToken)
        {
            await _businnessTasksService.DeleteBusinnessTasksAsync(request.Ids);
            return Unit.Value;
        }
    }
}
