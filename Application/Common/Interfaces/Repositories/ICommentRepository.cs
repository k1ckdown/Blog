using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment?> GetByIdIncludingAllAsync(Guid id);
    Task<Comment?> GetByIdIncludingSubCommentsAsync(Guid id);
}