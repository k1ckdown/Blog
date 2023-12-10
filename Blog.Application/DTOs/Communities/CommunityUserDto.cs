using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Communities;

public sealed class CommunityUserDto
{
    public required Guid UserId { get; set; }
    public required Guid CommunityId { get; set; }
    public required CommunityRole Role { get; set; }
}