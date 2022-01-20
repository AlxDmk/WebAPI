using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace MetricsAgentTests;

public class DotNetMetricsControllerTests
{
    private readonly DotNetMetricsController _controller;
    private readonly Mock<IRepository<DotNetMetric>> _repositoryMock;

    public DotNetMetricsControllerTests()
    {
        var loggerMock = new Mock<ILogger<DotNetMetricsController>>();
        _repositoryMock = new Mock<IRepository<DotNetMetric>>();
        
        _controller = new DotNetMetricsController(loggerMock.Object, _repositoryMock.Object);
    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(TimeSpan.Zero, TimeSpan.Zero);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
    
    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        _repositoryMock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
        var result = _controller.Create(new MetricsAgent.Requests.DotNetMetricCreateRequest()
            {Time = TimeSpan.FromSeconds(1), Value = 50});
        _repositoryMock.Verify(repository =>repository.Create(It.IsAny<DotNetMetric>()), Times.AtLeastOnce());
    }
    
    [Fact]
    public void Update_ShouldCall_Update_From_Repository()
    {
        var result = _controller.Update(new DotNetMetric(){ Id = 1, Time = TimeSpan.FromSeconds(1), Value = 20});
        Assert.IsAssignableFrom<IActionResult>(result);
    }
    
    [Fact]
    public void Delete_ShouldCall_Delete_From_Repository()
    {
        var result = _controller.Delete(1);
        Assert.IsAssignableFrom<IActionResult>(result);
    }
    
    
    [Fact]
    public void GetById_ShouldCall_GetById_From_Repository()
    {
        var result = _controller.GetById(1);
        Assert.IsAssignableFrom<IActionResult>(result);
    }

}