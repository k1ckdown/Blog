using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<Subscription?> GetSubscriptionAsync(Guid userId, Guid communityId) =>
        await DbContext.Subscriptions.FindAsync(userId, communityId);

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await DbContext.Subscriptions.AddAsync(subscription);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteSubscriptionAsync(Subscription subscription)
    { 
        DbContext.Subscriptions.Remove(subscription);
        await DbContext.SaveChangesAsync();
    }
    
    public new async Task<Community?> GetByIdAsync(Guid id) =>
        await DbContext.Communities
            .Include(community => community.Administrators)
            .Include(community => community.Subscribers)
            .FirstOrDefaultAsync(community => community.Id == id);
}