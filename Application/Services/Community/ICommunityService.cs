using Application.DTOs.Community;

namespace Application.Services.Community;

public interface ICommunityService
{
    CommunityRole? GetGreatestUserRole(Guid userId, Domain.Entities.Community community);
}