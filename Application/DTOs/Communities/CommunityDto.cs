using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Communities;

public class CommunityDto : IMapFrom<Community>
{
    public required Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public required string Name { get; set; }
    
    public string? Description { get; set; }

    [DefaultValue(DefaultIsClosed)] 
    public bool IsClosed { get; set; } = DefaultIsClosed;
    
    public required DateTime CreateTime { get; set; }

    [DefaultValue(DefaultSubscribersCount)]
    public int SubscribersCount { get; set; } = DefaultSubscribersCount;
    
    private const bool DefaultIsClosed = false;
    private const int DefaultSubscribersCount = 0;

    public virtual void Mapping(Profile profile)
    {
        profile.CreateMap<Community, CommunityDto>()
            .IncludeAllDerived()
            .ForMember(dest => dest.SubscribersCount, opt => opt.MapFrom(src => src.Subscribers.Count));
    }
}