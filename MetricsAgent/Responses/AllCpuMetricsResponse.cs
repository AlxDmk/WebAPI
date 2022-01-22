using System.Collections.Generic;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
        
    }
}