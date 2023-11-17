using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FloridaMan.Functions;

public class GetTodaysFloridaMan
{
    private readonly ISearchService _searchService;

    public GetTodaysFloridaMan(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [FunctionName("GetTodaysFloridaMan")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        var results = _searchService.CrawlTodaysFloridaMan();
        return new OkObjectResult(results);
    }
}
