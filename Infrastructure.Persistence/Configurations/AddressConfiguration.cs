using Domain.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(address => address.Id);

        builder.Property(address => address.Id).HasColumnName("id");
        builder.Property(address => address.ObjectId).HasColumnName("objectid");
        builder.Property(address => address.ObjectGuid).HasColumnName("objectguid");

        builder.Property(address => address.Name).HasColumnName("name");
        builder.Property(address => address.TypeName).HasColumnName("typename");

        builder.Property(address => address.IsActual).HasColumnName("isactual");
        builder.Property(address => address.IsActive).HasColumnName("isactive");

        builder.Property(address => address.Level).HasColumnName("level");
    }
}