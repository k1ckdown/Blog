using Domain.Common;

namespace Domain.Entities;

public sealed class Like : BaseEntity
{
    public User User { get; set; }
    
    public required Guid UserId { get; set; }
    public required Guid PostId { get; set; }
}