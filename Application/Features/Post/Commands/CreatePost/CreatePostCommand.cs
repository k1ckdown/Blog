using Application.DTOs.Post;
using Application.Features.Base.CreatePost;

namespace Application.Features.Post.Commands.CreatePost;

public sealed record CreatePostCommand(Guid UserId, CreatePostDto CreatePostDto)
    : BaseCreatePostCommand(UserId, CreatePostDto);