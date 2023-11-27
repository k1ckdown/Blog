using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.PostDTOs;

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
    
    public string? Image { get; set; }
    
    public Guid? AddressId { get; set; }
    
    [Required]
    [MinLength(1)]
    public required List<Guid> Tags { get; set; }
}