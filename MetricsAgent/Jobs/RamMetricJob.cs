using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob :IJob
    {
        private readonly IRepository<RamMetric> _repository;
        private readonly PerformanceCounter _ramCounter;

        public RamMetricJob(IRepository<RamMetric> repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            var ramUsage = Convert.ToInt32(_ramCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            
            _repository.Create(new RamMetric {Time = time, Value = ramUsage});
            
            return Task.CompletedTask;
        }
    }
}