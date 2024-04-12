using System.Text;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Microsoft.AspNetCore.Components;

namespace FloridaMan.Services;

public class QueryService : IQueryService
{
    private static CustomSearchAPIService? _googleService;

    public QueryService(CustomSearchAPIService googleService)
    {
        _googleService = googleService;
    }

    private readonly static List<string> filterTitles =
    [
        "birthday",
        "quiz",
        "quiz:",
        "Frequently Asked Questions",
        "Assess",
        "Basketball",
        "Football",
        "Sports",
        "Soccer",
        "Athletics",
        "Tennis",
    ];

    private static string ConcatFiltersToQuery()
    {
        var builder = new StringBuilder();
        foreach (var filterTitle in filterTitles)
        {
            builder.Append(" -intitle:");
            builder.Append('"');
            builder.Append(filterTitle);
            builder.Append('"');
        }
        return builder.ToString();
    }

    public async Task<List<Result>> ExecuteQuery(string query)
    {
        var listRequest = _googleService!.Cse.List();
        listRequest.Q = query + ConcatFiltersToQuery();
        listRequest.Num = 10;
        listRequest.Cx = Environment.GetEnvironmentVariable("GoogleCXToken");
        return [.. (await listRequest.ExecuteAsync()).Items];
    }
}