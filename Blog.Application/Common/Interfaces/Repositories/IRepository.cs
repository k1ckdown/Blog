using Blog.Domain.Common;

namespace Blog.Application.Common.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> Entities { get; }
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
}