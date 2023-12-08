using Application.DTOs.Posts;
using Application.Features.Base.CreatePost;

namespace Application.Features.Communities.Commands.CreateCommunityPost;

public sealed record CreateCommunityPostCommand(Guid UserId, CreatePostDto CreatePostDto, Guid CommunityId) 
    : BaseCreatePostCommand(UserId, CreatePostDto);