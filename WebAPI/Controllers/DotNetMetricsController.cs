using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
       public DotNetMetricsController(ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            
        }
        
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] double fromTime, 
            [FromRoute] double toTime)
        {
            _logger.LogInformation("** DotNetMetricsController LOGGER");
            return Ok();
        }
    }
}
