using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repositories;

public interface IPostRepository : IRepository<Post>
{
    Task<Post?> GetByIdIncludingLikes(Guid id);
    Task<Post?> GetByIdIncludingComments(Guid id);
    Task<Post?> GetByIdIncludingAllAsync(Guid id);
}