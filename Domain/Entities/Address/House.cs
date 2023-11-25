using Domain.Entities.Address.Enums;

namespace Domain.Entities.Address;

public sealed class House
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    
    public string? Number { get; set; }
    public HouseType? Type { get; set; }
    
    public int IsActual { get; set; }
    public int IsActive { get; set; }
    
    public string? FirstAdditionalNumber { get; set; }
    public string? SecondAdditionalNumber { get; set; }
    
    public AdditionalPartHouseType? FirstAdditionalType { get; set; }
    public AdditionalPartHouseType? SecondAdditionalType { get; set; }
}