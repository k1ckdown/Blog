using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public sealed class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}