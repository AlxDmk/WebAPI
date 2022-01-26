using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
       public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] double fromTime, 
            [FromRoute] double toTime)
        {
            _logger.LogInformation("*** HddMetricsController LOGGER");
            return Ok();
        }
    }
}
