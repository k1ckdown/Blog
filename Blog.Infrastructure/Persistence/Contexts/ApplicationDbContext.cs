using Blog.Domain.Entities;
using Blog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
    }
}