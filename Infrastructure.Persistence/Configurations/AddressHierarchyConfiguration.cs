using Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AddressHierarchyConfiguration : IEntityTypeConfiguration<AddressHierarchy>
{
    public void Configure(EntityTypeBuilder<AddressHierarchy> builder)
    {
        builder.ToTable("as_adm_hierarchy");
        builder.HasKey(hierarchy => hierarchy.Id);

        builder.Property(hierarchy => hierarchy.Id).HasColumnName("id");
        builder.Property(hierarchy => hierarchy.ObjectId).HasColumnName("objectid");
        builder.Property(hierarchy => hierarchy.ParentObjectId).HasColumnName("parentobjid");
        builder.Property(hierarchy => hierarchy.Path).HasColumnName("path");
    }
}