using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;

namespace FloridaMan.Services;

public class QueryService : IQueryService
{
    private static CustomSearchAPIService? _googleService;

    public QueryService(CustomSearchAPIService googleService)
    {
        _googleService = googleService;
    }

    public async Task<List<Result>> ExecuteQuery(string query)
    {
        var listRequest = _googleService!.Cse.List();
        listRequest.Q = query;
        listRequest.Num = 10;
        listRequest.Cx = Environment.GetEnvironmentVariable("GoogleCXToken");
        return (await listRequest.ExecuteAsync()).Items.ToList();
    }
}