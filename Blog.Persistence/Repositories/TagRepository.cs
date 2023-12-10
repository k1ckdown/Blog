using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;

namespace Blog.Persistence.Repositories;

public sealed class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}