using MetricsAgent.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class RamMetricsControllerTests
{
    private readonly RamMetricsController _controller;

    public RamMetricsControllerTests()
    {
        _controller = new RamMetricsController();
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}