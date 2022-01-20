using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("api/metrics/dotnet/errors-count")]
    
    public class DotNetMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            return Ok();
        }
    }
}