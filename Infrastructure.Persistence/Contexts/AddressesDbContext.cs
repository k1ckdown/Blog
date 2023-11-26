using Domain.Entities.Address;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public sealed class AddressesDbContext : DbContext
{
    public DbSet<House> Houses { get; set; } = null!;
    public DbSet<AddressElement> AddressElements { get; set; } = null!;
    public DbSet<AddressHierarchy> AddressHierarchies { get; set; } = null!;

    public AddressesDbContext(DbContextOptions<AddressesDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new HouseConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new AddressHierarchyConfiguration());
    }
}