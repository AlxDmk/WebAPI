using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsControllerTests
    {
        private readonly RamMetricsController _controller;

        public RamMetricsControllerTests()
        {
            _controller = new RamMetricsController();
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

}
