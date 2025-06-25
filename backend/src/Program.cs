using System.Reflection.Metadata.Ecma335;
using FloridaMan.Middlewares;
using FloridaMan.Services;
using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!;
        policy.SetIsOriginAllowedToAllowWildcardSubdomains()
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));

builder.Services.AddSingleton<IQueryService, QueryService>(sp =>
{
    return new QueryService(new CustomSearchAPIService(new BaseClientService.Initializer { ApiKey = Environment.GetEnvironmentVariable("GoogleAPIKey") }));
});
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddMemoryCache();

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Environment.SetEnvironmentVariable("GoogleAPIKey", File.ReadAllLines(".env")[0]);
    Environment.SetEnvironmentVariable("GoogleCXToken", File.ReadAllLines(".env")[1]);
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePages();
}
app.UseMiddleware<RequestBodyCachingMiddleware>();

app.UseCors();
app.MapControllers();

app.Use(async (context, next) => {
  Console.WriteLine(context.Request.Path);
  if (context.Request.Path == "/debug-routes") {
    EndpointDataSource? endpointDataSource = context.RequestServices.GetService<EndpointDataSource>();
    List<string?>? routes = endpointDataSource?.Endpoints.OfType<RouteEndpoint>()
      .Where(e => e.RoutePattern.RawText is not null
                  && !e.RoutePattern.RawText.StartsWith("_framework") 
                  && !e.RoutePattern.RawText.StartsWith("_content")
                  && !e.RoutePattern.RawText.StartsWith("/_blazor"))
      .Select(e => e.RoutePattern.RawText)
      .ToList();
    await context.Response.WriteAsync(string.Join("\n", routes ?? []));
    return;
  }
  await next();
});

app.UseHsts();

app.Run();