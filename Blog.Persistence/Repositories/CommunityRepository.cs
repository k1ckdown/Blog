using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public sealed class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public IQueryable<Subscription> Subscriptions => DbContext.Subscriptions;
    public IQueryable<CommunityAdmin> Administrators => DbContext.CommunityAdmins;

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await DbContext.Subscriptions.AddAsync(subscription);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task<Community?> GetByIdIncludingRequestsAndSubscribersAsync(Guid id) =>
        await DbContext.Communities
            .Include(community => community.Subscribers)
            .Include(community => community.Requests)
            .FirstOrDefaultAsync(community => community.Id == id);

    public async Task<Community?> GetByIdIncludingSubscribersAsync(Guid id) =>
        await DbContext.Communities
            .Include(community => community.Subscribers)
            .FirstOrDefaultAsync(community => community.Id == id);
    
    public async Task<Community?> GetByIdIncludingAllMembersAsync(Guid id) =>
        await DbContext.Communities
            .Include(community => community.Administrators)
            .Include(community => community.Subscribers)
            .FirstOrDefaultAsync(community => community.Id == id);
}