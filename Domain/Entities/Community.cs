using Domain.Common;

namespace Domain.Entities;

public sealed class Community : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; } 
    public required bool IsClosed { get; set; }
    public required DateTime CreateTime { get; set; }
    
    public IList<User>? Subscribers { get; set; }
    public IList<User>? Administrators { get; set; }
}