using Blog.Application.DTOs.Common;

namespace Blog.Application.DTOs.Posts;

public sealed class PostPagedListDto
{
    public required IEnumerable<PostDto> Posts { get; set; }
    public required PageInfoModel Pagination { get; set; }
}