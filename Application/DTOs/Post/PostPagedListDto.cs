using Application.DTOs.Common;

namespace Application.DTOs.Post;

public sealed class PostPagedListDto
{
    public required IEnumerable<PostDto> Posts { get; set; }
    public required PageInfoModel Pagination { get; set; }
}