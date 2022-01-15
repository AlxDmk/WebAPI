using MetricsAgent.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class NetworkMetricsControllerTests
{
    private readonly NetworkMetricsController _controller;

    public NetworkMetricsControllerTests()
    {
        _controller = new NetworkMetricsController();
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}