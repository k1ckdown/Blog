using System.Text;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

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
        
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.DisplayName().ToLower());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        var result = new StringBuilder();

        for (var i = 0; i < input.Length; i++)
        {
            if (i > 0 && char.IsUpper(input[i]))
            {
                result.Append('_');
            }
            
            result.Append(char.ToLower(input[i]));
        }

        return result.ToString();
    }
}