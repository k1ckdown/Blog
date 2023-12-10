using System.ComponentModel.DataAnnotations;

namespace Blog.Application.DTOs.Comments;

public sealed class CreateCommentDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public required string Content { get; set; }
    
    public Guid? ParentId { get; set; }
}