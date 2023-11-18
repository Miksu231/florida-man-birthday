using FloridaMan.Functions;
using FloridaMan.Models;
using FloridaMan.Services;
using FloridaMan.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FloridaMan.Tests.Functions;

public class GetTodaysFloridaManTests
{
    private readonly Mock<ISearchService> _mockSearchService = new();
    private readonly GetTodaysFloridaMan _getTodaysFloridaMan;

    public GetTodaysFloridaManTests()
    {
        _getTodaysFloridaMan = new GetTodaysFloridaMan(_mockSearchService.Object);
    }

    
    [Fact]
    public async Task GetTodaysFloridaMan_GivenHttpRequest_ReturnsOkObjectResult()
    {
        _mockSearchService.Setup(x => x.CrawlTodaysFloridaMan())
            .ReturnsAsync(new List<DisplayResult>());
        var request = HttpRequestHelpers.CreateHttpRequest("GET");

        Assert.IsAssignableFrom<OkObjectResult>(await _getTodaysFloridaMan.RunAsync(request));
    }
}