namespace Domain.Entities;

public sealed class CommunityAdmin
{
    public required Guid UserId { get; set; }
    public required Guid CommunityId { get; set; }
}