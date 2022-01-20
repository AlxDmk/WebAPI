using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MetricsManagerTests
{
    public class DotNetMetricsControllerTests
    {
        private readonly DotNetMetricsController _controller;

        public DotNetMetricsControllerTests()
        {
            _controller = new DotNetMetricsController(NullLogger<DotNetMetricsController>.Instance);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
          
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}