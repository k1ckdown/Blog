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

        builder
            .HasOne(post => post.User)
            .WithMany(user => user.Posts)
            .HasForeignKey(post => post.UserId);

        builder
            .HasMany(post => post.FavoriteByUsers)
            .WithMany(user => user.FavoritePosts)
            .UsingEntity<FavoritePost>(
                l => l.HasOne<User>().WithMany().HasForeignKey(e => e.UserId),
                r => r.HasOne<Post>().WithMany().HasForeignKey(e => e.PostId));
    }
}