using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Application.DTOs.Common;

namespace Blog.Application.DTOs.Posts;

public sealed class PostSearchParameters : PaginationParameters
{
    public IList<Guid>? Tags { get; set; }
    
    public string? Author { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Min { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Max { get; set; }
    
    public PostSorting? Sorting { get; set; }

    [DefaultValue(DefaultOnlyMyCommunities)]
    public bool OnlyMyCommunities { get; set; } = DefaultOnlyMyCommunities;
    
    private const bool DefaultOnlyMyCommunities = false;
}