using Application.DTOs.Post;
using Application.Features.Base.CreatePost;

namespace Application.Features.Community.Commands.CreateCommunityPost;

public sealed record CreateCommunityPostCommand(Guid UserId, CreatePostDto CreatePostDto, Guid CommunityId) 
    : BaseCreatePostCommand(UserId, CreatePostDto);