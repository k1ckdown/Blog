using Domain.Common;

namespace Domain.Entities;

public sealed class Tag : BaseEntity
{
    public required string Name { get; set; }
    public required DateTime CreateTime { get; set; }
    public List<Post> Posts { get; set; } = new();
}