using AutoMapper;
using Blog.Application.DTOs.Comments;
using Blog.Domain.Entities;

namespace Blog.Application.DTOs.Posts;

public sealed class PostFullDto : PostDto
{
    public required IEnumerable<CommentDto> Comments { get; set; }

    public override void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostFullDto>();
    }
}