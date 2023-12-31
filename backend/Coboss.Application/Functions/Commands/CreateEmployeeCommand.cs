using MediatR;

namespace Coboss.Application.Functions.Commands
{
    public class CreateEmployeeCommand : IRequest<Unit>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string? PESEL { get; set; }
        public string? NIP { get; set; }
        public DateTime DateOfBirth { get; set; } = default!;
    }
}
