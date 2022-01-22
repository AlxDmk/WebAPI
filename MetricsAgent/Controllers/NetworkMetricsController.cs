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
    [Route("api/metrics/network")]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IRepository<NetworkMetric> _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IRepository<NetworkMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            _logger.LogInformation("+++ NetworkMetricsController LOGGER");
            return Ok();
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromForm] NetworkMetricCreateRequest request)
        {
            _repository.Create(new NetworkMetric()
            {
                Time = request.Time,
                Value = request.Value
                
            });
            _logger.LogError("+++ NetworkMetricsController CREATE LOGGER ");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            _logger.LogError("+++ NetworkMetricsController GETALL LOGGER ");
            return Ok(response);
        }
        
        [HttpPut("update")]
        public IActionResult Update([FromForm] NetworkMetric request)
        {
            _repository.Update(request);
            _logger.LogInformation("+++ NetworkMetricsController Update LOGGER");
            
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            _logger.LogInformation("+++ NetworkMetricsController Delete LOGGER");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _repository.GetById(id);
            var response = new NetworkMetricDto();
            _mapper.Map(result, response);
            _logger.LogInformation("+++ NetworkMetricsController GetById LOGGER");

            return Ok(response);
        }
    }
}