using Coboss.Types.DTO;
using MediatR;

namespace Coboss.Application.Functions.Query.Projects
{
    public class GetProjectsQuery : IRequest<List<ProjectDTO>>
    {
        public string? SearchText { get; set; }
        public string? OrderBy { get; set; }
    }
}
