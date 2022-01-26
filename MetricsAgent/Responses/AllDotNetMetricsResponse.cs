using System.Collections.Generic;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
        
    }
}