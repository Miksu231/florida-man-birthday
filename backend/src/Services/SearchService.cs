using FloridaMan.Models;
using Google.Apis.CustomSearchAPI.v1.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FloridaMan.Services;

public partial class SearchService(IQueryService queryService) : ISearchService
{
    private readonly IQueryService _queryService = queryService;

    public async Task<List<DisplayResult>> CrawlTodaysFloridaMan()
    {
        var search = await _queryService.ExecuteQuery($"florida+man+{DateTime.Today.Month.ToString().ToLower()}+{DateTime.Today.Day.ToString().ToLower()}");
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

    public async Task<List<DisplayResult>> CrawlDateFloridaMan(string month, string day)
    {
        var search = await _queryService.ExecuteQuery($"florida+man+{month.ToLower()}+{day.ToLower()}");
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
        if (searchResult.Pagemap?.TryGetValue("metatags", out var output) ?? false && output is not null)
        {
            var jsonResult = JsonConvert.SerializeObject(output);
            var metaData = JsonConvert.DeserializeObject<List<Metatags>>(jsonResult)?.FirstOrDefault(new Metatags { Description = null, Image = null, Title = null});
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

    private static List<DisplayResult> FilterResults(List<DisplayResult> results)
    {
        return results
            .Where(result => !result.Title.Split(" ").Intersect(FilterTitles, StringComparer.OrdinalIgnoreCase).Any())
            .Where(result => !result.Link.Split(".").Intersect(FilterURLs, StringComparer.OrdinalIgnoreCase).Any())
            .Where(result => !FilterRegex.Any(regex => regex.IsMatch(result.Title)))
            .ToList();
    }

    [GeneratedRegex(@"\.\.\.")]
    private static partial Regex ThreedotRegex();
    [GeneratedRegex(@"Published\s[0-9]{4}")]
    private static partial Regex PublishedRegex();

    private readonly static List<string> FilterURLs  =
    [
        "reddit",
        "pinterest",
        "linkedin"
    ];

    private readonly static List<string> FilterTitles  =
    [
        "birthday",
        "quiz",
        "quiz:"
    ];

    private readonly static List<Regex> FilterRegex =
    [
        ThreedotRegex(),
        PublishedRegex()
    ];
}