using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder
            .HasMany(community => community.Administrators);

        builder
            .HasMany(community => community.Subscribers)
            .WithMany(subscriber => subscriber.Subscriptions)
            .UsingEntity(joinEntity => joinEntity.ToTable("Subscribers"));
    }
}