using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;

        public NetworkMetricsControllerTests()
        {
            var loggerMock = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(loggerMock.Object);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            var result = _controller.GetMetricsFromAgent(1, 1643055099, 1643098155);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

}
