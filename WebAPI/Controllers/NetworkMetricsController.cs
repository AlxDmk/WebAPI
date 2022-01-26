using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  System;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] double fromTime, 
            [FromRoute] double toTime)
        {
            _logger.LogInformation("**** NetworkMetricsController LOGGER");
            return Ok();
        }
    }
}
