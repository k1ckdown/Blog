using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public sealed class CommunityRepository : Repository<Community>, ICommunityRepository
{
    public CommunityRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}