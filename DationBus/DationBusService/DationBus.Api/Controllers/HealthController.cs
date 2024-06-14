using Microsoft.AspNetCore.Mvc;

namespace DationBus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("health");
        }
    }
}
