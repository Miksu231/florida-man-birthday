using FloridaMan.Models;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FloridaMan.Services;

public class SearchService : ISearchService
{
    private static CustomSearchAPIService? _googleService;

    public SearchService(CustomSearchAPIService service)
    {
        _googleService = service;
    }

    public List<DisplayResult> CrawlTodaysFloridaMan()
    {
        var search = ExecuteSearch($"florida+man+{DateTime.Today.Month.ToString().ToLower()}+{DateTime.Today.Day.ToString().ToLower()}");
        var resultList = new List<DisplayResult>();
        foreach (var result in search)
        {
            if (result is not null)
            {
                resultList.Add(ExtractMetadata(result));
            }
        }
        return FilterResults(resultList);
    }

    public List<DisplayResult> CrawlDateFloridaMan(string month, string day)
    {
        var search = ExecuteSearch($"florida+man+{month.ToLower()}+{day.ToLower()}");
        var resultList = new List<DisplayResult>();

        foreach (var result in search)
        {
            if (result is not null)
            {
                resultList.Add(ExtractMetadata(result));
            }
        }
        return FilterResults(resultList);
    }

    private static DisplayResult ExtractMetadata(Result searchResult)
    {
        if (searchResult.Pagemap.TryGetValue("metatags", out var output) && output is not null)
        {
            var jsonResult = JsonConvert.SerializeObject(output);
            var metaData = JsonConvert.DeserializeObject<List<Metatags>>(jsonResult)?.First();
            return new DisplayResult
            { 
                Title = metaData?.Title ?? searchResult.Title,
                Link = searchResult.Link,
                ImageLink = metaData?.Image ?? "",
                Snippet = metaData?.Description
            };
        }
        else
        {
            return new DisplayResult {
                Title = searchResult.Title,
                Link = searchResult.Link,
            };
        }
    }

    private static IList<Result> ExecuteSearch(string query)
    {
        var listRequest = _googleService!.Cse.List();
        listRequest.Q = query;
        listRequest.Num = 10;
        listRequest.Cx = Environment.GetEnvironmentVariable("GoogleCXToken");
        return listRequest.Execute().Items;
    }

    private static List<DisplayResult> FilterResults(List<DisplayResult> results)
    {
        return results
            .Where(result => !result.Title.Split(" ").Intersect(FilterTitles, StringComparer.OrdinalIgnoreCase).Any())
            .Where(result => !result.Link.Split(".").Intersect(FilterURLs, StringComparer.OrdinalIgnoreCase).Any())
            .Where(result => !FilterRegex.Any(regex => regex.IsMatch(result.Title)))
            .ToList();
    }

    private readonly static List<string> FilterURLs  = new()
    {
        "reddit",
        "pinterest"
    };

    private readonly static List<string> FilterTitles  = new()
    {
        "birthday",
        "...",
        "(Published"
    };

    private readonly static List<Regex> FilterRegex = new()
    {
        new Regex(@"\.\.\."),
        new Regex(@"Published\s[0-9]{4}")
    };
}