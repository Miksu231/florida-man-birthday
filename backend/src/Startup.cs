using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FloridaMan.Services;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;

[assembly: FunctionsStartup(typeof(FloridaMan.Startup))]
namespace FloridaMan;

public class Startup: FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
		builder.Services.AddSingleton<IQueryService, QueryService>(sp => 
		{
			return new QueryService(new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GoogleAPIKey") }));
		});
		builder.Services.AddScoped<ISearchService, SearchService>();
    }
}