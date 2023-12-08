using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICommunityRepository : IRepository<Community>
{
    IQueryable<Subscription> Subscriptions { get; }
    IQueryable<CommunityAdmin> Administrators { get; }
    Task<Community?> GetByIdIncludingAllMembersAsync(Guid id);
    Task AddSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetSubscriptionAsync(Guid userId, Guid communityId);
}