using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Responses.Models;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [ApiController]
    [Route("api/metrics/dotnet/errors-count")]
    
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IRepository<DotNetMetric> _repository;

        private  readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IRepository<DotNetMetric> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime )
        {
            _logger.LogError("+++ DotNetMetricsController LOGGER ");
            return Ok();
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromForm] DotNetMetricCreateRequest request)
        {
            _repository.Create(new DotNetMetric
            {
                Time = request.Time,
                Value = request.Value
                
            });
            _logger.LogError("+++ DotNetMetricsController CREATE LOGGER ");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };
            foreach (var metric in metrics)
            {

                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            };        

            _logger.LogError("+++ DotNetMetricsController GETALL LOGGER ");
            return Ok(response);
        }
        
        [HttpPut("update")]
        public IActionResult Update([FromForm] DotNetMetric request)
        {
            _repository.Update(request);
            _logger.LogInformation("+++ DotNetMetricsController Update LOGGER");
            

            return Ok();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            _logger.LogInformation("+++ DotNetMetricsController Delete LOGGER");

            return Ok();
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _repository.GetById(id);

            var response = new DotNetMetricDto();

            _mapper.Map(result, response);
            
            _logger.LogInformation("+++ DotNetMetricsController GetById LOGGER");

            return Ok(response);

        }

    }
}