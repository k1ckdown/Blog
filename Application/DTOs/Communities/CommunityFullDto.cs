using System.ComponentModel.DataAnnotations;
using Application.DTOs.Account;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Communities;

public sealed class CommunityFullDto : CommunityDto
{
    [Required]
    public required List<UserDto> Administrators { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Community, CommunityFullDto>();
    }
}