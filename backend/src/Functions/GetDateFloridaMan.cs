using FloridaMan.Extensions;
using FloridaMan.Models;
using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace FloridaMan.Functions;

public class GetDateFloridaMan
{
    private readonly ISearchService _searchService;

    public GetDateFloridaMan(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [FunctionName("GetDateFloridaMan")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
    {
        var httpResponseBody = await req.GetBodyAsync<DateDto>();
        if (!httpResponseBody.IsValid)
        {
            return new BadRequestResult();
        }
        
        var results = await _searchService.CrawlDateFloridaMan(httpResponseBody.Value!.Month, httpResponseBody.Value.Day);
        return new OkObjectResult(results);
    }
}
