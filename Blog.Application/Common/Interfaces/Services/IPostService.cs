using Blog.Application.DTOs.Posts;
using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces.Services;

public interface IPostService
{
    IQueryable<Post> FilterByAuthor(IQueryable<Post> posts, string author);
    IQueryable<Post> FilterByMinReadingTime(IQueryable<Post> posts, int minTime);
    IQueryable<Post> FilterByMaxReadingTime(IQueryable<Post> posts, int maxTime);
    IQueryable<Post> FilterByTags(IQueryable<Post> posts, IList<Guid> tagIdentifiers);
    IQueryable<Post> Sort(IQueryable<Post> posts, PostSorting? sortingType);
    List<PostDto> ToListDto(Guid userId, List<Post> posts);
    Task<PostPagedListDto> ToPagedList(IQueryable<Post> posts, int page, int size, Guid userId);
}