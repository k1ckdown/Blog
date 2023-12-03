namespace Domain.Entities;

public sealed class Like
{
    public User User { get; set; }
    
    public required Guid UserId { get; set; }
    public required Guid PostId { get; set; }
}