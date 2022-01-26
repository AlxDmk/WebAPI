using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.DAL.Models;
using System;
using Core.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _controller;
        

        public DotNetMetricsControllerTests()
        {
            var loggerMock = new Mock<ILogger<DotNetMetricsController>>();
            _controller = new DotNetMetricsController(loggerMock.Object);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            var result = _controller.GetMetricsFromAgent(1, 1643055099, 1643098155);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}