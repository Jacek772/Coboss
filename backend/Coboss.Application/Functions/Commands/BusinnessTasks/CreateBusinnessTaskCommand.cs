using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTasks
{
    public class CreateBusinnessTaskCommand : IRequest<Unit>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Date { get; set; }
        public DateTime Term { get; set; }
        public int ProjectId { get; set; }
        public CreateBusinnessTaskCommentCommand[] Comments { get; set; } = default!;
        public CreateBusinnessTaskRealisationCommand[] NewTaskRealisations { get; set; } = default!;
    }
}
