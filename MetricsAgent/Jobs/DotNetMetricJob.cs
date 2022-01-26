using System;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IRepository<DotNetMetric> _repository;
       
        public DotNetMetricJob(IRepository<DotNetMetric> repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            _repository.Create(new DotNetMetric
            {
                Value = (int)GC.GetTotalMemory(false),
                Time = time
            });
            
            return Task.CompletedTask;
        }
    }
}