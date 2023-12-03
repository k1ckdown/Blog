using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<User?> GetByIdIncludingCommunitiesAsync(Guid id) =>
        await DbContext.Users
            .Include(user => user.Subscriptions)
            .Include(user => user.AdministeredCommunities)
            .FirstOrDefaultAsync(user => user.Id == id);
}