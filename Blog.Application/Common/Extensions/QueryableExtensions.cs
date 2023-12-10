using Blog.Application.Common.Exceptions.Base;

namespace Blog.Application.Common.Extensions;

public static class QueryableExtensions
{
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