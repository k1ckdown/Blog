using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public new async Task<Community?> GetByIdAsync(Guid id) =>
        await DbContext.Communities
            .Include(community => community.Administrators)
            .FirstOrDefaultAsync(community => community.Id == id);
}