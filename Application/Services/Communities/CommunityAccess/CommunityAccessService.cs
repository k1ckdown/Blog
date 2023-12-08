using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Communities.CommunityAccess;

public sealed class CommunityAccessService : ICommunityAccessService
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityRepository _communityRepository;

    public CommunityAccessService(IPostRepository postRepository, ICommunityRepository communityRepository)
    {
        _postRepository = postRepository;
        _communityRepository = communityRepository;
    }

    public async Task CheckAccess(Guid userId, Community community)
    {
        if (await IsAccessAllowed(userId, community)) return;
        throw new ForbiddenException($"Access to closed community ({community.Id}) is forbidden for user ({userId})");
    }

    public Task CheckAccessToPost(Guid userId, Post post)
    {
        return post.CommunityId == null 
            ? Task.CompletedTask
            : CheckAccess(userId, post.CommunityId.Value, post.Id, RestrictedCommunityAccessObjectType.Post);
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
        var community = await _communityRepository.GetByIdAsync(communityId);
        if (community == null) throw new NotFoundException(nameof(Community), communityId);
        
        if (await IsAccessAllowed(userId, community)) return;
        throw new ForbiddenException(
            $"Access to closed community {objectType.ToString().ToLower()} ({objectId}) is forbidden for user ({userId})");
    }

    private async Task<bool> IsAccessAllowed(Guid userId, Community community)
    {
        if (community.IsClosed == false) return true;

        if (community.Administrators?.Any(admin => admin.Id == userId)
            ?? await _communityRepository.Administrators
                .AnyAsync(admin => admin.UserId == userId && admin.CommunityId == community.Id)) return true;
        
        return community.Subscribers?.Any(subscriber => subscriber.Id == userId) 
               ?? await _communityRepository.Subscriptions
                   .AnyAsync(subscription => subscription.UserId == userId && subscription.CommunityId == community.Id);
    }
}