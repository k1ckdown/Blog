using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
   public CommentRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}