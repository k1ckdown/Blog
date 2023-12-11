namespace Blog.Domain.Entities;

public sealed class CommunityRequest
{
    public required Guid UserId { get; set; }
    public required Guid CommunityId { get; set; }
    public required DateTime CreateTime { get; set; }

    public User User { get; set; }
    public Community Community { get; set; }
}