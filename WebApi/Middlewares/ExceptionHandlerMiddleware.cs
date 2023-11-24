using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Application.Common.Exceptions;
using Application.Wrappers;

namespace WebApi.Middlewares;

public sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            var statusCode = exception switch
            {
                BadRequestException => HttpStatusCode.BadRequest,
                InvalidCredentialException => HttpStatusCode.Unauthorized,
                ValidationException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
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