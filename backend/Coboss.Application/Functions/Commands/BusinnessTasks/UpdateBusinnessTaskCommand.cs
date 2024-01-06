using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTasks
{
    public class UpdateBusinnessTaskCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Term { get; set; }
    }
}
