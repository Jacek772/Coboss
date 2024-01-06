using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Services.Abstracts;
using MediatR;

namespace Coboss.Application.Functions.CommandHandlers.BusinnessTaskComments
{
    public class UpdateBusinnessTaskCommentCommandHandler : IRequestHandler<UpdateBusinnessTaskCommentCommand, Unit>
    {
        private readonly IBusinnessTaskCommentsService _businnessTaskCommentsService;

        public UpdateBusinnessTaskCommentCommandHandler(IBusinnessTaskCommentsService businnessTaskCommentsService)
        {
            _businnessTaskCommentsService = businnessTaskCommentsService;
        }

        public async Task<Unit> Handle(UpdateBusinnessTaskCommentCommand request, CancellationToken cancellationToken)
        {
            await _businnessTaskCommentsService.UpdateAsync(request);
            return Unit.Value;
        }
    }
}
