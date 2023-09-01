using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;
using Google.Apis.CustomSearchAPI.v1.Data;
using System.Linq;

namespace FloridaMan;

public class SearchService : ISearchService
{
    private static CustomSearchAPIService _googleService;
    public SearchService(CustomSearchAPIService service)
    {
        _googleService = service;
    }

    public List<DisplayResult> CrawlTodaysFloridaMan()
    {
        var date = new { DateTime.Today.Day, DateTime.Today.Month };
        var query = $"florida+man+{date.Month.ToString().ToLower()}+{date.Day.ToString().ToLower()}";
        var listRequest = _googleService.Cse.List();
        listRequest.Q = query;
        listRequest.Num = 5;
        listRequest.Cx = Environment.GetEnvironmentVariable("GoogleCXToken");
        var search = listRequest.Execute().Items.ToList();
        var resultList = new List<DisplayResult>();
        foreach (var result in search)
        {
            var success = result.Pagemap.TryGetValue("metatags", out var output);
            if (success)
            {
                var jsonData = JsonConvert.SerializeObject(output);
                var metaData = JsonConvert.DeserializeObject<List<Metatags>>(jsonData)!.First();
                resultList.Add(new DisplayResult{ Title = metaData.Title ?? result.Title, Link = result.Link, ImageLink = metaData.Image ?? "", Snippet = metaData.Description });
            }
        }
        return resultList;
    }
}