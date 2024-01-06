using MediatR;

namespace Coboss.Application.Functions.Commands.Projects
{
    public class DeleteProjectsCommand : IRequest<Unit>
    {
        public int[] Ids { get; set; } = default!;
    }
}
