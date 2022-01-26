using System;

namespace MetricsAgent.Responses.Models
{
    public class NetworkMetricDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}