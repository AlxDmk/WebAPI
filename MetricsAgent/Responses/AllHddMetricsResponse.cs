using System.Collections.Generic;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
        
    }
}