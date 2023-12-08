using Application.DTOs.Posts;
using Application.Features.Base.CreatePost;

namespace Application.Features.Posts.Commands.CreatePost;

public sealed record CreatePostCommand(Guid UserId, CreatePostDto CreatePostDto)
    : BaseCreatePostCommand(UserId, CreatePostDto);