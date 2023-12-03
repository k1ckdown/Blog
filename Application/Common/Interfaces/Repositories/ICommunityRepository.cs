using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICommunityRepository : IRepository<Community>
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetSubscriptionAsync(Guid userId, Guid communityId);
}