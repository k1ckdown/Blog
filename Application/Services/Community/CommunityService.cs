using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Community;

public sealed class CommunityService : ICommunityService
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityRepository _communityRepository;

    public CommunityService(IPostRepository postRepository, ICommunityRepository communityRepository)
    {
        _postRepository = postRepository;
        _communityRepository = communityRepository;
    }

    public async Task CheckAccessToPost(Guid userId, Post post)
    {
        if (post.CommunityId != null)
            await CheckAccess(userId, post.CommunityId.Value, post.Id, RestrictedCommunityAccessObjectType.Post);
    }
    
    public async Task CheckAccessToComment(Guid userId, Comment comment)
    {
        var post = await _postRepository.GetByIdAsync(comment.PostId);
        if (post is { CommunityId: not null })
            await CheckAccess(userId, post.CommunityId.Value, comment.Id, RestrictedCommunityAccessObjectType.Comment);
    }
    
    private async Task CheckAccess(
        Guid userId,
        Guid communityId,
        Guid objectId,
        RestrictedCommunityAccessObjectType objectType)
    {
        if ((await _communityRepository.Administrators
                 .AnyAsync(admin => admin.UserId == userId && admin.CommunityId == communityId) 
             || await _communityRepository.Subscriptions
                 .AnyAsync(subscription => subscription.UserId == userId && subscription.CommunityId == communityId)) == false)
        {
            throw new ForbiddenException(
                $"Access to closed community {objectType.ToString().ToLower()} ({objectId}) is forbidden for user ({userId})");
        }
    }
}