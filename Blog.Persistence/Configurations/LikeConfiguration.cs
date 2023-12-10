using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Persistence.Configurations;

public sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder
            .HasKey(like => new { like.UserId, like.PostId });
    }
}