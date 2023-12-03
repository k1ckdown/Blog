using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Application.Common.Exceptions;
using Application.Wrappers;

namespace WebApi.Middlewares;

public sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ForbiddenException => HttpStatusCode.Forbidden,
                BadRequestException => HttpStatusCode.BadRequest,
                InvalidCredentialException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            
            var response = new Response(statusCode.ToString(), exception.Message);
            var result = JsonSerializer.Serialize(response);
            
            await context.Response.WriteAsync(result);
        }
    }
}