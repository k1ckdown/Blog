using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repositories;

public interface ICommunityRepository : IRepository<Community>
{
    IQueryable<Subscription> Subscriptions { get; }
    IQueryable<CommunityAdmin> Administrators { get; }
    Task AddSubscriptionAsync(Subscription subscription);
    Task<Community?> GetByIdIncludingAllMembersAsync(Guid id);
    Task<Community?> GetByIdIncludingSubscribersAsync(Guid id);
    Task<Community?> GetByIdIncludingRequestsAndAdminsAsync(Guid id);
    Task<Community?> GetByIdIncludingRequestsAndSubscribersAsync(Guid id);
}