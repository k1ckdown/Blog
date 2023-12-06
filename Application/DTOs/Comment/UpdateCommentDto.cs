using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Comment;

public sealed class UpdateCommentDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public required string Content { get; set; }
}