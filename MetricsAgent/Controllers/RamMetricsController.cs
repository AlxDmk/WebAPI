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
    [Route("api/metrics/ram/available")]
    public class RamMetricsController : Controller
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRepository<RamMetric> _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRepository<RamMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            _logger.LogInformation("+++ RamMetricsController");
            return Ok();
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromForm] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric()
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogError("+++ RamMetricsController CREATE  LOGGER");
            return Ok();
        }

        
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            _logger.LogError("+++ RamMetricsController GET ALL LOGGER");
            
            return Ok(response);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] RamMetric request)
        {
            _repository.Update(request);
            _logger.LogInformation("+++ RamMetricsController Update LOGGER");
            
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            _logger.LogInformation("+++ RamMetricsController Delete LOGGER");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _repository.GetById(id);
            var response = new RamMetricDto();
            _mapper.Map(result, response);
            
            _logger.LogInformation("+++ RamMetricsController GetById LOGGER");

            return Ok(response);
        }

    }
}