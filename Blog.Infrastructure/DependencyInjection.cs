using Blog.Application.Common.Interfaces.Services;
using Blog.Application.Interfaces.Repositories;
using Blog.Infrastructure.Identity.Authentication;
using Blog.Infrastructure.Identity.Authentication.ConfigureOptions;
using Blog.Infrastructure.Identity.Contexts;
using Blog.Infrastructure.Identity.Models;
using Blog.Infrastructure.Identity.Services;
using Blog.Infrastructure.Persistence.Contexts;
using Blog.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthDbContext(configuration);
        services.AddApplicationDbContext(configuration);
        services.AddRepositories();

        services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AuthDbContext>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.ConfigureOptions<ConfigureIdentityOptions>();
        services.ConfigureOptions<ConfigureJwtOptions>();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddScoped<JwtProvider>();
        services.AddScoped<IAccountService, AccountService>();
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    private static void AddAuthDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityConnection");
        services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString));
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }
}