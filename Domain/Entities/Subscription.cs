namespace Domain.Entities;

public sealed class Subscription
{
    public required Guid UserId { get; set; }
    public required Guid CommunityId { get; set; }
}