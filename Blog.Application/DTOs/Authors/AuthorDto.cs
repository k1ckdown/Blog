using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Authors;

public sealed class AuthorDto : IMapFrom<User>
{
    public required string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public required int Posts { get; set; }
    public required int Likes { get; set; }
    public required DateTime Created { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, AuthorDto>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.CreateTime))
            .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts.Count))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Posts.SelectMany(post => post.Likes).Count()));
    }
}