using Blog.Application.Common.Interfaces.Services;
using Blog.WebApi.Extensions;

namespace Blog.WebApi.Middlewares;

public sealed class JwtSessionMiddleware : IMiddleware
{
    private readonly IAuthService _authService;
    
    public JwtSessionMiddleware(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var accessToken = context.Request.BearerToken();

        if (string.IsNullOrEmpty(accessToken) == false && await _authService.IsRevoked(accessToken))
        {
            context.Response.StatusCode = 401;
            return;
        }

        await next(context);
    }
}