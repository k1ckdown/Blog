namespace Domain.Entities.Address;

public sealed class AddressHierarchy
{
    public long Id { get; set; }
    public long ObjectId { get; set; }
    public long ParentObjectId { get; set; }
    public required string Path { get; set; }
}