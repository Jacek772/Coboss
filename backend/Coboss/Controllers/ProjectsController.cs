using Coboss.Controllers.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers
{
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : BaseApiController
    {
        public ProjectsController(IMediator mediator) : base(mediator)
        {
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
