using System.ComponentModel.DataAnnotations;
using Application.DTOs.Account;
using AutoMapper;

namespace Application.DTOs.Community;

public sealed class CommunityFullDto : CommunityDto
{
    [Required]
    public required List<UserDto> Administrators { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Community, CommunityFullDto>();
    }
}