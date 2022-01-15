using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class NetworkMetricsControllerTests
    {
        private readonly NetworkMetricsController _controller;

        public NetworkMetricsControllerTests()
        {
            _controller = new NetworkMetricsController();
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

}
