using Microsoft.Net.Http.Headers;

namespace WebApi.Extensions;

public static class HttpRequestExtensions
{
    public static string BearerToken(this HttpRequest request) => 
        request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
}