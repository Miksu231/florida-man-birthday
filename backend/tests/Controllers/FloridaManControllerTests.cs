using FloridaMan.Controllers;
using FloridaMan.Models;
using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;

namespace FloridaMan.Tests.Functions;

public class FloridaManControllerTests
{
    private readonly Mock<ISearchService> _mockSearchService = new();
    private readonly FloridaManController _controller;

    public FloridaManControllerTests()
    {
        _controller = new FloridaManController(_mockSearchService.Object);
    }


    [Fact]
    public async Task GetTodaysFloridaMan_ReturnsOkObjectResult()
    {
        _mockSearchService.Setup(x => x.CrawlTodaysFloridaMan())
            .ReturnsAsync([]);

        Assert.IsAssignableFrom<OkObjectResult>(await _controller.GetTodaysFloridaMan());
    }

    [Fact]
    public async Task GetDateFloridaMan_GivenValidDate_ReturnsOkObjectResult()
    {
        _mockSearchService.Setup(x => x.CrawlDateFloridaMan(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync([]);
        var date = new DateDto { Day = "15", Month = "March" };

        Assert.IsAssignableFrom<OkObjectResult>(await _controller.GetDateFloridaMan(date));
    }
}