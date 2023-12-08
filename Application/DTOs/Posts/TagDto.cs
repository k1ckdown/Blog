using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DTOs.Posts;

public sealed class TagDto : IMapFrom<Tag>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreateTime { get; set; }
}