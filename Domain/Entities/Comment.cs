using Domain.Common;

namespace Domain.Entities;

public sealed class Comment : BaseEntity
{
    public required string Content { get; set; }
    public DateTime? DeleteDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required DateTime CreateTime { get; set; }
    public IList<Comment> SubComments { get; set; }

    public User User { get; set; }
    public required Guid UserId { get; set; }
}