using Domain.Entities;

namespace Application.Services.Community;

public sealed class CommunityService : ICommunityService
{
    public CommunityRole? GetGreatestUserRole(Guid userId, Domain.Entities.Community community)
    {
        if (community.Administrators.Any(administrator => administrator.Id == userId))
            return CommunityRole.Administrator;

        if (community.Subscribers.Any(subscriber => subscriber.Id == userId))
            return CommunityRole.Subscriber;

        return null;
    }
}