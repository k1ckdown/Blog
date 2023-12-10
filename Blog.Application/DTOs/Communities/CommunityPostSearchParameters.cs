using Blog.Application.DTOs.Common;
using Blog.Application.DTOs.Posts;

namespace Blog.Application.DTOs.Communities;

public sealed class CommunityPostSearchParameters : PaginationParameters
{
    public required IList<Guid>? Tags { get; set; }
    public PostSorting? Sorting { get; set; }
}