using Blog.Domain.Entities;
using Blog.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Contexts;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<CommunityRequest> CommunityRequests { get; set; } = null!;
    
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<FavoritePost> FavoritePosts { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<CommunityAdmin> CommunityAdmins { get; set; } = null!;
    
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Community> Communities { get; set; } = null!;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
        builder.ApplyConfiguration(new LikeConfiguration());
        builder.ApplyConfiguration(new SubscriptionConfiguration());
        builder.ApplyConfiguration(new CommunityConfiguration());
        builder.ApplyConfiguration(new CommunityAdminConfiguration());
        builder.ApplyConfiguration(new CommunityRequestConfiguration());
    }
}