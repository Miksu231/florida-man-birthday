using FloridaMan.Functions;
using FloridaMan.Models;
using FloridaMan.Services;
using FloridaMan.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FloridaMan.Tests.Functions;

public class GetDateFloridaManTests
{
    private readonly Mock<ISearchService> _mockSearchService = new();
    private readonly GetDateFloridaMan _getDateFloridaMan;

    public GetDateFloridaManTests()
    {
        _getDateFloridaMan = new GetDateFloridaMan(_mockSearchService.Object);
    }

    
    [Fact]
    public async Task GetDateFloridaMan_GivenValidDate_ReturnsOkObjectResult()
    {
        _mockSearchService.Setup(x => x.CrawlDateFloridaMan(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new List<DisplayResult>());
        var request = HttpRequestHelpers.CreateHttpRequest("GET", JsonConvert.SerializeObject(new DateDto{ Day = "15", Month = "March"}));

        Assert.IsAssignableFrom<OkObjectResult>(await _getDateFloridaMan.RunAsync(request));
    }

    [Fact]
    public async Task GetDateFloridaMan_GivenInvalidDate_ReturnsBadRequestResult()
    {
        _mockSearchService.Setup(x => x.CrawlDateFloridaMan(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new List<DisplayResult>());
        var request = HttpRequestHelpers.CreateHttpRequest("GET", JsonConvert.SerializeObject(new DateDto{ Month = "March"}));

        Assert.IsAssignableFrom<BadRequestResult>(await _getDateFloridaMan.RunAsync(request));
    }
}