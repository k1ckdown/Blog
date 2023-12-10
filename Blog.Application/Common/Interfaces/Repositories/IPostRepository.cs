using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task AddLikeAsync(Like like);
    Task DeleteLikeAsync(Like like);
    Task<Like?> GetLikeAsync(Guid userId, Guid postId);
    Task<Post?> GetByIdIncludingComments(Guid id);
    Task<Post?> GetByIdIncludingAllAsync(Guid id);
}