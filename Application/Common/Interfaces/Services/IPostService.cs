using Application.DTOs.Posts;
using Domain.Entities;

namespace Application.Common.Interfaces.Services;

public interface IPostService
{
    IQueryable<Post> FilterByAuthor(IQueryable<Post> posts, string author);
    IQueryable<Post> FilterByMinReadingTime(IQueryable<Post> posts, int minTime);
    IQueryable<Post> FilterByMaxReadingTime(IQueryable<Post> posts, int maxTime);
    IQueryable<Post> FilterByTags(IQueryable<Post> posts, IList<Guid> tagIdentifiers);
    IQueryable<Post> Sort(IQueryable<Post> posts, PostSorting? sortingType);
    Task<PostPagedListDto> ToPagedList(IQueryable<Post> posts, int page, int size, Guid userId);
}