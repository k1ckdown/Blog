using Application.DTOs.Comments;
using AutoMapper;
using Domain.Entities;

namespace Application.DTOs.Posts;

public sealed class PostFullDto : PostDto
{
    public required IEnumerable<CommentDto> Comments { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostFullDto>();
    }
}