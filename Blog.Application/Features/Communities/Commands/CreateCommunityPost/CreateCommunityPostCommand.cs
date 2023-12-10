using Blog.Application.DTOs.Posts;
using Blog.Application.Features.Base.CreatePost;

namespace Blog.Application.Features.Communities.Commands.CreateCommunityPost;

public sealed record CreateCommunityPostCommand(Guid UserId, CreatePostDto CreatePostDto, Guid CommunityId) 
    : BaseCreatePostCommand(UserId, CreatePostDto);