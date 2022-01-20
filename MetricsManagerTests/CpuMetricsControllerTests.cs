﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MetricsManagerTests
{
    public class CpuMetricsControllerTests
    {
        private readonly CpuMetricsController _controller;

        public CpuMetricsControllerTests()
        {
            _controller = new CpuMetricsController(NullLogger<CpuMetricsController>.Instance);
        }
        
        [Fact]
        public void GetMetricsFromAgent_ReturnOK()
        {
            var result = _controller.GetMetricsFromAgent(1, TimeSpan.Zero, TimeSpan.Zero);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

}
