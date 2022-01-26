using AutoMapper;
using Core.DAL.Interfaces;
using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace MetricsAgentTests;

public class CpuMetricsControllerTests
{
    private readonly CpuMetricsController _controller;
    private readonly Mock<IRepository<CpuMetric>> _repositoryMock;

    public CpuMetricsControllerTests()
    {
        var loggerMock = new Mock<ILogger<CpuMetricsController>>();
         _repositoryMock = new Mock<IRepository<CpuMetric>>();
         var mapperMock = new Mock<IMapper>();
         _controller = new CpuMetricsController(loggerMock.Object, _repositoryMock.Object, mapperMock.Object);

    }
    
    [Fact]
    public void GetMetrics_ReturnOK()
    {
        var result = _controller.GetMetrics(1643055099, 1643098155);
        Assert.IsAssignableFrom<IActionResult>(result);
    }

    [Fact]
    public void Create_ShouldCall_Create_From_Repository()
    {
        _repositoryMock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
        var result = _controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest
            {Time = TimeSpan.FromSeconds(1), Value = 50});
        _repositoryMock.Verify(repository =>repository.Create(It.IsAny<CpuMetric>()), Times.AtLeastOnce());
    }


    [Fact]
    public void Update_ShouldCall_Update_From_Repository()
    {
        var result = _controller.Update(new CpuMetric(){ Id = 1, Time = TimeSpan.FromSeconds(1), Value = 20});
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



