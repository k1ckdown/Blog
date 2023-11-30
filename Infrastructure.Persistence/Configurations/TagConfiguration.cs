using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasData(
                CreateTag("cbbee647-d1db-4b9b-b053-f9f640bb97d8", "it"),
                CreateTag("3f34aaa1-b6be-432d-9ffc-aee460e3b7d7", "18+"),
                CreateTag("cada0a21-a535-4126-ae94-b3f0f1171415", "соцсети"),
                CreateTag("542a25a1-9b47-4b43-a1b3-8e98018fd5ab", "интернет"),
                CreateTag("f8bf2f1b-48bb-4749-b984-0c9856093ba0", "история"),
                CreateTag("3070c757-f147-4576-947e-3c0d89494fcb", "приколы"),
                CreateTag("0f7740e0-8fed-4721-bc0e-7d2eac48434e", "косплей"),
                CreateTag("4a239805-e276-455e-b0a1-ad3b2958ac70", "преступление"),
                CreateTag("3fc1a0fb-b836-4eb2-83b8-9d56eb8d93d5", "еда"),
                CreateTag("a80d3dd8-b6c1-4d85-959f-1433ab1523b5", "теория заговора"));
    }

    private static Tag CreateTag(string id, string name) =>
        new Tag
        {
            Id = new Guid(id),
            Name = name,
            CreateTime = DateTime.UtcNow
        };
}