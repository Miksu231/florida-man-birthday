using System.Text;
using Microsoft.AspNetCore.Http;

namespace FloridaMan.Tests.Helpers;

public class HttpRequestHelpers
{
    public static HttpRequest CreateHttpRequest(string method, string? content = null)
    {
        var request = new Mock<HttpRequest>();
        request.Setup(r => r.Method).Returns(method);

        if (content != null)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            request.Setup(r => r.Body).Returns(stream);
        }
        return request.Object;
    }
}