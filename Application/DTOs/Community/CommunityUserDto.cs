using Domain.Entities;

namespace Application.DTOs.Community;

public sealed class CommunityUserDto
{
    public required Guid UserId { get; set; }
    public required Guid CommunityId { get; set; }
    public required CommunityRole Role { get; set; }
}