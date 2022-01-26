using System.Collections.Generic;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
        
    }
}