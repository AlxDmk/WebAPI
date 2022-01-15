using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
namespace MetricsManagerTests
{
    public class HddMetricsControllerTests
    {
        private readonly HddMetricsController _controller;

        public HddMetricsControllerTests()
        {
            _controller = new HddMetricsController();
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
           
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);

            Assert.IsAssignableFrom<IActionResult>(result);

        }
    }

}
