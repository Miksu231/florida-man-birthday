using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace FloridaMan.Middlewares;

public class RequestBodyCachingMiddleware(RequestDelegate next, IMemoryCache cache)
{

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        string requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        string cacheKey = ComputeHash(requestBody);

        if (cache.TryGetValue(cacheKey, out string? cachedResponse))
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(cachedResponse!);
            return;
        }

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string responseBodyContent = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        cache.Set(cacheKey, responseBodyContent, TimeSpan.FromDays(1));

        await responseBody.CopyToAsync(originalBodyStream);
    }

    private static string ComputeHash(string input)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hashBytes);
    }
}