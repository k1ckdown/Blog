using Blog.Application.Common.Exceptions.Base;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<Post> IncludeAll(this IQueryable<Post> items) => 
        items
            .Include(post => post.User)
            .Include(post => post.Tags)
            .Include(post => post.Likes)
            .Include(post => post.Comments)
            .Include(post => post.Community)
            .Include(post => post.FavoriteByUsers);
    
    public static (IQueryable<T>, int) ToPaged<T>(this IQueryable<T> items, int pageNumber, int pageSize)
    {
        var totalPages = (int)Math.Ceiling(items.Count() / (pageSize * 1.0));
        if (pageNumber > totalPages && items.Any()) throw new BadRequestException("Invalid value for attribute page");

        var paged = items
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        return (paged, totalPages);
    }
}