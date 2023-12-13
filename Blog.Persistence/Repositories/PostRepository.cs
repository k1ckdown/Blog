using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Persistence.Repositories;

public sealed class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext) : base(dbContext) {}
    
    public async Task AddFavoritePostAsync(FavoritePost favoritePost)
    {
        await DbContext.AddAsync(favoritePost);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task<Post?> GetByIdIncludingLikes(Guid id) => 
        await Entities
            .Include(post => post.Likes)
            .FirstOrDefaultAsync(post => post.Id == id);
    
    public async Task<Post?> GetByIdIncludingComments(Guid id) => 
        await Entities
            .Include(post => post.Comments)
            .FirstOrDefaultAsync(post => post.Id == id);

    public async Task<Post?> GetByIdIncludingFavoriteByUsers(Guid id) =>
        await Entities
            .Include(post => post.FavoriteByUsers)
            .FirstOrDefaultAsync(post => post.Id == id);

    public async Task<Post?> GetByIdIncludingAllAsync(Guid id) => 
        await Entities
            .Include(post => post.User)
            .Include(post => post.Likes)
            .Include(post => post.Community)
            .Include(post => post.Tags)
            .Include(post => post.Comments)
                .ThenInclude(comment => comment.User)
            .FirstOrDefaultAsync(post => post.Id == id);
}