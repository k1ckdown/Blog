using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

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
    
    public async Task<Post?> GetByIdIncludingCommunity(Guid id) =>
        await DbContext.Posts
            .Include(post => post.Community)
            .FirstOrDefaultAsync(post => post.Id == id);
}