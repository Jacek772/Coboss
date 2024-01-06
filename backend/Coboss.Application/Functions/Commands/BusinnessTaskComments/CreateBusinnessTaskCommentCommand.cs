using MediatR;

namespace Coboss.Application.Functions.Commands.BusinnessTaskComments
{
    public class CreateBusinnessTaskCommentCommand : IRequest<Unit>
    {
        public string Text { get; set; } = default!;
        public DateTime Date { get; set; }
        public int UserId { get; set; } = default!;
        public int TaskId { get; set; } = default!;
    }
}
