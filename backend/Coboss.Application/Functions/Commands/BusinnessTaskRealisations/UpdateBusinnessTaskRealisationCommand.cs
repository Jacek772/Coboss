using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskRealisations
{
    public class UpdateBusinnessTaskRealisationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? TimeSpan { get; set; }
        public string? Description { get; set; }
    }
}
