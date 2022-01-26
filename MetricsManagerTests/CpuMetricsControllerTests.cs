using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;
        public CpuMetricsControllerTests()
        {
            var loggerMock = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(loggerMock.Object);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            var result = _controller.GetMetricsFromAgent(1, 1643055099, 1643098155);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    
}
