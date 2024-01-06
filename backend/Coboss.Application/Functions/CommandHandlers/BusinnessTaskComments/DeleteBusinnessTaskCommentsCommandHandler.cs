using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskComments
{
    public class DeleteBusinnessTaskCommentsCommandHandler : IRequestHandler<DeleteBusinnessTaskCommentsCommand, Unit>
    {
        private readonly IBusinnessTaskCommentsService _businnessTaskCommentsService;

        public DeleteBusinnessTaskCommentsCommandHandler(IBusinnessTaskCommentsService businnessTaskCommentsService)
        {
            _businnessTaskCommentsService = businnessTaskCommentsService;
        }

        public async Task<Unit> Handle(DeleteBusinnessTaskCommentsCommand request, CancellationToken cancellationToken)
        {
            await _businnessTaskCommentsService.DeletesAsync(request.Ids);
            return Unit.Value;
        }
    }
}
