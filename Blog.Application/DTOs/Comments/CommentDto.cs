using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Comments;

public sealed class CommentDto : IMapFrom<Comment>
{
    public required Guid Id { get; set; }
    public required string Content { get; set; }

    public DateTime? DeleteDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public required DateTime CreateTime { get; set; }

    public required Guid AuthorId { get; set; }
    public required string Author { get; set; }
    public required int SubComments { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.SubComments, opt => opt.MapFrom(src => src.SubComments.Count));
    }
}