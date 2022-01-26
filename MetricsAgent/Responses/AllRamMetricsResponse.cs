using System.Collections.Generic;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Responses
{
    public class AllRamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
        
    }
}