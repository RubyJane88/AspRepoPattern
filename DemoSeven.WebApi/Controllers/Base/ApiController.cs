using DemoSeven.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DemoSeven.WebApi.Controllers.Base
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        
    }
}