using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

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