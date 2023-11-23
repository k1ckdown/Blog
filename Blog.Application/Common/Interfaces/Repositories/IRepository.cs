using Blog.Domain.Common.Interfaces;

namespace Blog.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> Entities { get; }
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<T?> GetByIdAsync(Guid id);
}