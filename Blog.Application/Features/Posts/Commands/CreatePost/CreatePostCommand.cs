using Blog.Application.DTOs.Posts;
using Blog.Application.Features.Base.CreatePost;

namespace Blog.Application.Features.Posts.Commands.CreatePost;

public sealed record CreatePostCommand(Guid UserId, CreatePostDto CreatePostDto)
    : BaseCreatePostCommand(UserId, CreatePostDto);