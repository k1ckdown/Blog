using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
   public CommentRepository(ApplicationDbContext dbContext) : base(dbContext) {}

   public async Task<Comment?> GetByIdIncludingSubCommentsAsync(Guid id) =>
       await DbContext.Comments
           .Include(comment => comment.SubComments)
           .FirstOrDefaultAsync(comment => comment.Id == id);
   
   public async Task<Comment?> GetByIdIncludingAllAsync(Guid id) =>
       await DbContext.Comments
           .Include(comment => comment.User)
           .Include(comment => comment.SubComments)
           .FirstOrDefaultAsync(comment => comment.Id == id);
}