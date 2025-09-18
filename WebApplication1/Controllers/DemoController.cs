using Microsoft.AspNetCore.Mvc;
using WebApplication1.MyLogging;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : Controller
    {
        private readonly ILogger<DemoController> myLogger;
        public DemoController(ILogger<DemoController> _myLogger)
        {
            myLogger = _myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            myLogger.LogTrace("Log Message From Trace");
            myLogger.LogDebug("Log Message From Debug");
            myLogger.LogInformation("Log Message From Information");
            myLogger.LogWarning("Log Message From Warning");
            myLogger.LogError("Log Message From Error");
            myLogger.LogCritical("Log Message From Critical");

            return Ok();
        }
    }
}
