using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Queries.GetPostList;

public sealed class GetPostListQuery : IRequest<PostPagedListDto>
{
    public IList<Guid>? Tags { get; set; }
    
    public string? Author { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Min { get; set; }
    
    [Range(0, int.MaxValue)]
    public int? Max { get; set; }
    
    public PostSorting? Sorting { get; set; }
    
    [DefaultValue(false)]
    public bool OnlyMyCommunities { get; set; }
    
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Page { get; set; }
    
    [DefaultValue(5)]
    [Range(1, int.MaxValue)]
    public int Size { get; set; }
}