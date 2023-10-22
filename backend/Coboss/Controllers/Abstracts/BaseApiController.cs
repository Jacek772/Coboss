using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Coboss.Controllers.Abstracts
{
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {

    }
}
