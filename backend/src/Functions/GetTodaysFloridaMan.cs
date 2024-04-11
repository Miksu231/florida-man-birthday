using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FloridaMan.Functions;

public class GetTodaysFloridaMan
{
    private readonly ISearchService _searchService;

    public GetTodaysFloridaMan(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [FunctionName("GetTodaysFloridaMan")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
    {
        var results = await _searchService.CrawlTodaysFloridaMan();
        return new OkObjectResult(results);
    }
}
