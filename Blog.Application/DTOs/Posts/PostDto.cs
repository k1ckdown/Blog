using System.ComponentModel;
using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Posts;

public class PostDto : IMapFrom<Post>
{
    public required Guid Id { get; set; }
    public required DateTime CreateTime { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int ReadingTime { get; set; }
    public string? Image { get; set; }
    public required Guid AuthorId { get; set; }
    public required string Author { get; set; }
    public Guid? CommunityId { get; set; }
    public string? CommunityName { get; set; }
    public Guid? AddressId { get; set; }
    
    [DefaultValue(DefaultLikes)]
    public int Likes { get; set; } = DefaultLikes;

    [DefaultValue(DefaultHasLike)] 
    public bool HasLike { get; set; } = DefaultHasLike;

    [DefaultValue(DefaultCommentsCount)]
    public int CommentsCount { get; set; } = DefaultCommentsCount;
    
    public required IEnumerable<TagDto> Tags { get; set; }

    private const int DefaultLikes = 0;
    private const bool DefaultHasLike = false;
    private const int DefaultCommentsCount = 0;

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostDto>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.CommunityName,
                opt => opt.MapFrom(src => src.Community == null ? null : src.Community.Name));
    }
}