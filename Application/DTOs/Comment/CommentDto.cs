using Application.Common.Mappings;
using AutoMapper;

namespace Application.DTOs.Comment;

public sealed class CommentDto : IMapFrom<Domain.Entities.Comment>
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
        profile.CreateMap<Domain.Entities.Comment, CommentDto>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.SubComments, opt => opt.MapFrom(src => src.SubComments.Count));
    }
}