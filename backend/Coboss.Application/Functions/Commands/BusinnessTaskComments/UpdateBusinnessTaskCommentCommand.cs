using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskComments
{
    public class UpdateBusinnessTaskCommentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
    }
}
