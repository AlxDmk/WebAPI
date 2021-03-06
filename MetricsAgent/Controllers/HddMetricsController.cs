using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("api/metrics/hdd/left")]
    
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IRepository<HddMetric> _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IRepository<HddMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        
        
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            _logger.LogError("+++ HddMetricsController LOGGER");
            return Ok();
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromForm] HddMetricCreateRequest request)
        {
            _repository.Create(new HddMetric()
            {
                Time = request.Time,
                Value = request.Value
                
            });
            _logger.LogError("+++ HddMetricsController CREATE LOGGER ");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };
            foreach (var metric in metrics)
            {

                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));

            }
            _logger.LogError("+++ HddMetricsController GetAll LOGGER ");
            return Ok(response);
        }
        
        [HttpPut("update")]
        public IActionResult Update([FromForm] HddMetric request)
        {
            _repository.Update(request);
            _logger.LogInformation("+++ HddMetricsController Update LOGGER");
            
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            _logger.LogInformation("+++ HddMetricsController Delete LOGGER");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _repository.GetById(id);

            var response = new HddMetricDto();
            _mapper.Map(result, response);
            _logger.LogInformation("+++ HddMetricsController GetById LOGGER");

            return Ok(response);

        }
    }
}