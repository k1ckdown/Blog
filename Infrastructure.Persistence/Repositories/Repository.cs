using Application.Common.Interfaces.Repositories;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext DbContext;
    
    protected Repository(ApplicationDbContext dbContext) =>
        DbContext = dbContext;

    public IQueryable<T> Entities => DbContext.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }
    
    public async Task UpdateAsync(T entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task AddAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
    }
}