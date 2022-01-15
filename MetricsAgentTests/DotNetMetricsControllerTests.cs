using MetricsAgent.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class DotNetMetricsControllerTests
{
    private readonly DotNetMetricsController _controller;

    public DotNetMetricsControllerTests()
    {
        _controller = new DotNetMetricsController();
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}