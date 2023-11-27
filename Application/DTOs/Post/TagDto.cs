using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DTOs.Post;

public sealed class TagDto : IMapFrom<Tag>
{
    public required Guid Id { get; set; }
    
    [MinLength(1)]
    public required string Name { get; set; }
    
    public required DateTime CreateTime { get; set; }
}