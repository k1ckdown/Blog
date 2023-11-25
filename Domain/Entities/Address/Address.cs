using Domain.Entities.Address.Enums;

namespace Domain.Entities.Address;

public sealed class Address
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    
    public required string Name { get; set; }
    public required string TypeName { get; set; }
    
    public int IsActual { get; set; }
    public int IsActive { get; set; }
    
    public GarAddressLevel Level { get; set; }
}