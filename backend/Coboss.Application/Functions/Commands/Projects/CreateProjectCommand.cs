using MediatR;

namespace Coboss.Application.Functions.Commands.Projects
{
    public class CreateProjectCommand : IRequest<Unit>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime Term { get; set; } = default!;
        public int ManagerId { get; set; } = default!;
    }
}
