using MediatR;

namespace Coboss.Application.Functions.Commands.Projects
{
    public class UpdateProjectCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Term { get; set; }
        public int? ManagerId { get; set; }
    }
}
