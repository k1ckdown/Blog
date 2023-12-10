namespace Blog.Domain.Entities.Address;

public sealed class AddressElement
{
    public long Id { get; set; }
    public long ObjectId { get; set; }
    public Guid ObjectGuid { get; set; }
    
    public required string Name { get; set; }
    public required string TypeName { get; set; }
    
    public required string Level { get; set; }
}