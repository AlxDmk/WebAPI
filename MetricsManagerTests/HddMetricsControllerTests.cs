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
    public class HddMetricsControllerTests
    {
        private readonly HddMetricsController _controller;

        public HddMetricsControllerTests()
        {
            var loggerMock = new Mock<ILogger<HddMetricsController>>();
            _controller = new HddMetricsController(loggerMock.Object);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
           
            var result = _controller.GetMetricsFromAgent(1, 1643055099, 1643098155);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

}
