using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Blog.Application.DTOs.Account;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Communities;

public sealed class CommunityFullDto : CommunityDto
{
    [Required]
    public required List<UserDto> Administrators { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Community, CommunityFullDto>();
    }
}