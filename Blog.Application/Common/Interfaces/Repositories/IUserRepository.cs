using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdIncludingCommunitiesAsync(Guid id);
}