using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
using MetricsAgent.Responses.Models;

namespace MetricsAgent.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
        }
        
    }
}