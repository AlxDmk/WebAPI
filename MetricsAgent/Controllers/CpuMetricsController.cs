using System.Collections.Generic;
using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Responses.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("api/metrics/cpu")]

    public class CpuMetricsController : ControllerBase
    {
        private readonly IRepository<CpuMetric> _repository;
        
        private readonly ILogger<CpuMetricsController> _logger;

        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepository<CpuMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] double fromTime, [FromRoute] double toTime)
        {
            _logger.LogInformation("+++ CpuMetricsController LOGGER");
            return Ok(_repository.Select(fromTime, toTime));
        }

        
        [HttpPost("create")]
        public IActionResult Create([FromForm] CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation("+++ CpuMetricsController CREATE  LOGGER");
            return Ok();
        }

        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            
            var metrics = _repository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            
            _logger.LogInformation("+++ CpuMetricsController GET ALL LOGGER");
            
            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] CpuMetric request)
        {
            _repository.Update(request);
            _logger.LogInformation("+++ CpuMetricsController Update LOGGER");
            
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            _logger.LogInformation("+++ CpuMetricsController Delete LOGGER");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _repository.GetById(id);

            var response = new CpuMetricDto();
            _mapper.Map(result,response);
            
            _logger.LogInformation("+++ CpuMetricsController GetById LOGGER");

            return Ok(response);
        }

    }

}