using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Posts;

public sealed class CreatePostDto
{
    [Required]
    [MinLength(5)]
    public required string Title { get; set; }
    
    [Required]
    [MinLength(5)]
    public required string Description { get; set; }
    
    [Required]
    public required int ReadingTime { get; set; }
    
    [Url]
    public string? Image { get; set; }
    
    public Guid? AddressId { get; set; }
    
    [Required]
    [MinLength(1)]
    public required IList<Guid> Tags { get; set; }
}