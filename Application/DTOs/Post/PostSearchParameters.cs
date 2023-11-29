using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Post;

public sealed class PostSearchParameters
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

    [DefaultValue(DefaultPage)]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = DefaultPage;

    [DefaultValue(DefaultSize)]
    [Range(1, int.MaxValue)]
    public int Size { get; set; } = DefaultSize;

    private const int DefaultPage = 1;
    private const int DefaultSize = 5;
    private const bool DefaultOnlyMyCommunities = false;
}