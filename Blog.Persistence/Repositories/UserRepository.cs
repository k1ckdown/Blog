using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<User?> GetByIdIncludingCommunitiesAsync(Guid id) =>
        await DbContext.Users
            .Include(user => user.Subscriptions)
            .Include(user => user.AdministeredCommunities)
            .FirstOrDefaultAsync(user => user.Id == id);
}