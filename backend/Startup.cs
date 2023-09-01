using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;

[assembly: FunctionsStartup(typeof(FloridaMan.Startup))]
namespace FloridaMan;

public class Startup: FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
		builder.Services.AddSingleton<ISearchService, SearchService>(sp => 
		{
			return new SearchService(new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GoogleAPIKey") }));
		});
    }
}