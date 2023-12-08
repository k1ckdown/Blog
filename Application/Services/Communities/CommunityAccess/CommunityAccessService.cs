using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
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
        throw new CommunityAccessException(userId, community.Id);
    }

    public async Task CheckAccessToPost(Guid userId, Post post)
    {
        if (post.CommunityId == null) return;

        var community = post.Community ?? await _communityRepository.GetByIdAsync(post.CommunityId.Value);
        if (community == null) throw new NotFoundException(nameof(Community), post.CommunityId);
        
        await CheckAccess(userId, community, post.Id, RestrictedCommunityAccessObjectType.Post);
    }
    
    public async Task CheckAccessToComment(Guid userId, Comment comment)
    {
        var post = await _postRepository.GetByIdAsync(comment.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), comment.PostId);

        if (post.CommunityId == null) return;
        
        var community = await _communityRepository.GetByIdAsync(post.CommunityId.Value);
        if (community == null) throw new NotFoundException(nameof(Community), post.CommunityId);
        
        await CheckAccess(userId, community, comment.Id, RestrictedCommunityAccessObjectType.Comment);
    }
    
    private async Task CheckAccess(
        Guid userId, Community community, Guid objectId,
        RestrictedCommunityAccessObjectType objectType)
    {
        if (await IsAccessAllowed(userId, community)) return;
        throw new CommunityAccessException(userId, objectId, objectType);
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