using MetricsAgent.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests;

public class CpuMetricsControllerTests
{
    private readonly CpuMetricsController _controller;

    public CpuMetricsControllerTests()
    {
        _controller = new CpuMetricsController();
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
}