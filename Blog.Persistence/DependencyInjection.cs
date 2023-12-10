using Blog.Application.Common.Interfaces.Repositories;
using Blog.Persistence.Contexts;
using Blog.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Persistence;

public static class DependencyInjection
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDbContext(configuration);
        services.AddAddressesDbContext(configuration);
        services.AddRepositories();
    }

    private static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    private static void AddAddressesDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GarConnection");
        services.AddDbContext<AddressesDbContext>(options => options.UseNpgsql(connectionString));
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommunityRepository, CommunityRepository>();
    }
}