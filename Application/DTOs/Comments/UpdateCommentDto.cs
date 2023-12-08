using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Comments;

public sealed class UpdateCommentDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public required string Content { get; set; }
}