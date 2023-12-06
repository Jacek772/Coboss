using Coboss.Application.Services.Abstracts;
using Coboss.Controllers.Abstracts;
using Coboss.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/employee")]
    [Authorize]
    public class EmployeesController : BaseApiController
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService, IMediator mediator) : base(mediator)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            return await _employeesService.GetEmployeesAsync();
        }
    }
}
