using MetricsAgent.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class HddMetricsControllerTests
{
    private readonly HddMetricsController _controller;

    public HddMetricsControllerTests()
    {
        _controller = new HddMetricsController();
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}