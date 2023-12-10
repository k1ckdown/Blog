using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Persistence.Configurations;

public sealed class CommunityRequestConfiguration : IEntityTypeConfiguration<CommunityRequest>
{
    public void Configure(EntityTypeBuilder<CommunityRequest> builder)
    {
        builder.HasKey(request => new { request.UserId, request.CommunityId });
    }
}