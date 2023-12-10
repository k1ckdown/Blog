using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Persistence.Configurations;

public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasMany(post => post.Tags)
            .WithMany(tag => tag.Posts);
    }
}