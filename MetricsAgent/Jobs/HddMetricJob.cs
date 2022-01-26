using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IRepository<HddMetric> _repository;
        private readonly PerformanceCounter _hddCounter;

        public HddMetricJob(IRepository<HddMetric> repository)
        {
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "0 C:");
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var hddUsage = Convert.ToInt32(_hddCounter.NextValue());
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            
            _repository.Create(new HddMetric {Time = time, Value = hddUsage});
            
            return Task.CompletedTask;
        }
    }
}