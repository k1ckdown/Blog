namespace Blog.Domain.Entities;

public sealed class FavoritePost
{
    public required Guid UserId { get; set; }
    public required Guid PostId { get; set; }
}