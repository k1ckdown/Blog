using AutoMapper;
using Blog.Application.Common.Mappings;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Communities;

public sealed class CommunityRequestDto : IMapFrom<CommunityRequest>
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required DateTime CreateTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunityRequest, CommunityRequestDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.FullName));
    }
}