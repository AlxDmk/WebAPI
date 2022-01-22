using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace MetricsAgentTests;

public class NetworkMetricsControllerTests
{
    private readonly NetworkMetricsController _controller;
    private readonly Mock<IRepository<NetworkMetric>> _repositoryMock;

    public NetworkMetricsControllerTests()
    {
        var loggerMock = new Mock<ILogger<NetworkMetricsController>>();
        _repositoryMock = new Mock<IRepository<NetworkMetric>>();
        var mapperMock = new Mock<IMapper>();
        _controller = new NetworkMetricsController(loggerMock.Object, _repositoryMock.Object, mapperMock.Object);
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
        _repositoryMock.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();
        var result = _controller.Create(new MetricsAgent.Requests.NetworkMetricCreateRequest()
            {Time = TimeSpan.FromSeconds(1), Value = 50});
        _repositoryMock.Verify(repository =>repository.Create(It.IsAny<NetworkMetric>()), Times.AtLeastOnce());
    }
    
    [Fact]
    public void Update_ShouldCall_Update_From_Repository()
    {
        var result = _controller.Update(new NetworkMetric(){ Id = 1, Time = TimeSpan.FromSeconds(1), Value = 20});
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