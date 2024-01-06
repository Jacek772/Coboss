using MediatR;

namespace Coboss.Application.Functions.Commands.Employees
{
    public class UpdateEmployeeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PESEL { get; set; }
        public string? NIP { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
