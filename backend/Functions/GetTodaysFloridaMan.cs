using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace FloridaMan
{
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
            var filteredResults = results.Where(x => !x.Title?.Contains("Florida Man Birthday") ?? true);
            return new OkObjectResult(filteredResults);
        }
    }
}
