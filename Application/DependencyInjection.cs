using System.Reflection;
using Application.Common.Interfaces.Services;
using Application.Services;
using Application.Services.Communities;
using Application.Services.Communities.CommunityAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

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