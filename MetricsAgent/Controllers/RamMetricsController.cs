using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("ram/available")]
    public class RamMetricsController : Controller
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            return Ok();
        }
    }
}