namespace Domain.Entities.Address;

public sealed class AddressHierarchy
{
    public int Id { get; set; }
    public int ObjectId { get; set; }
    public int ParentObjectId { get; set; }
    public required string Path { get; set; }
}