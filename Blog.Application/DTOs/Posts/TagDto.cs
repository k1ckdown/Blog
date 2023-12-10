using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Posts;

public sealed class TagDto : IMapFrom<Tag>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreateTime { get; set; }
}