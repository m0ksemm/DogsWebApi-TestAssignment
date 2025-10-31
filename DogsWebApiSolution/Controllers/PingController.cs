using Microsoft.AspNetCore.Mvc;

namespace DogsWebApi.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Content("Dogshouseservice.Version1.0.1", "text/plain");
    }
}
