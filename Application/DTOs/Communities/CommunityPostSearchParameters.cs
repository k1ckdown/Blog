using Application.DTOs.Common;
using Application.DTOs.Posts;

namespace Application.DTOs.Communities;

public sealed class CommunityPostSearchParameters : PaginationParameters
{
    public required IList<Guid>? Tags { get; set; }
    public PostSorting? Sorting { get; set; }
}