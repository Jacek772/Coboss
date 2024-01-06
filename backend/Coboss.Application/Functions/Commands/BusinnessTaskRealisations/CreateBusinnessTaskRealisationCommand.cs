using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskRealisations
{
    public class CreateBusinnessTaskRealisationCommand : IRequest<Unit>
    {
        public DateTime Date { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string? Description { get; set; }
        public int TaskId { get; set; }
    }
}
