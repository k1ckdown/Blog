using System.Reflection;
using Blog.Application.Services.Communities;
using Blog.Application.Common.Interfaces.Services;
using Blog.Application.Services;
using Blog.Application.Services.Communities.CommunityAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddServices();
    }
    
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICommunityAccessService, CommunityAccessService>();
        services.AddScoped<IPostService, PostService>();
    }
}