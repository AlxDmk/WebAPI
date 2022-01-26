using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob :IJob
    {
        private readonly IRepository<NetworkMetric> _repository;
        private readonly PerformanceCounter _networkCounter;

        public NetworkMetricJob(IRepository<NetworkMetric> repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter(
                "Network Interface",
                "Bytes Received/sec",
                "TP-Link Wireless USB Adapter"
            );
        }

        public Task Execute(IJobExecutionContext context)
        {
            var networkUsage = Convert.ToInt32(_networkCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            
            _repository.Create(new NetworkMetric{Time = time, Value = networkUsage});
            
            return Task.CompletedTask;
        }
    }
}