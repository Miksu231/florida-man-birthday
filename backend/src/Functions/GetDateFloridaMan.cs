using FloridaMan.Models;
using FloridaMan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var date = JsonConvert.DeserializeObject<DateDto>(requestBody);
        var results = _searchService.CrawlDateFloridaMan(date!.Month, date.Day);
        var filteredResults = results.Where(x => !x.Title?.Contains("Florida Man Birthday") ?? true);
        return new OkObjectResult(filteredResults);
    }
}
