using Microsoft.Net.Http.Headers;

namespace Blog.WebApi.Extensions;

public static class HttpRequestExtensions
{
    public static string BearerToken(this HttpRequest request) => 
        request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
}