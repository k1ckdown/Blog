using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public sealed class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}