using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity
{
    public required string Content { get; set; }
    public DateTime? DeleteDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required DateTime CreateTime { get; set; }
    public List<Comment> SubComments { get; set; } = new();

    public User User { get; set; }
    public required Guid UserId { get; set; }
    
    public Comment Parent { get; set; }
    public Guid? ParentId { get; set; }
    
    public required Guid PostId { get; set; }
}