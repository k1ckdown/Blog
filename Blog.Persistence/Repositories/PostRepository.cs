using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public sealed class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext) : base(dbContext) {}
    
    public async Task AddLikeAsync(Like like)
    {
        await DbContext.Likes.AddAsync(like);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteLikeAsync(Like like)
    {
        DbContext.Likes.Remove(like);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task<Like?> GetLikeAsync(Guid userId, Guid postId) =>
        await DbContext.Likes
            .FindAsync(userId, postId);
    
    public async Task<Post?> GetByIdIncludingComments(Guid id) =>
        await DbContext.Posts
            .Include(post => post.Comments)
            .FirstOrDefaultAsync(post => post.Id == id);

    public async Task<Post?> GetByIdIncludingAllAsync(Guid id) =>
        await DbContext.Posts
            .Include(post => post.User)
            .Include(post => post.Likes)
            .Include(post => post.Community)
            .Include(post => post.Tags)
            .Include(post => post.Comments)
                .ThenInclude(comment => comment.User)
            .FirstOrDefaultAsync(post => post.Id == id);
}