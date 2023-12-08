using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.Services.Communities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Commands.DislikePost;

public sealed class DislikePostCommandHandler : IRequestHandler<DislikePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public DislikePostCommandHandler(IPostRepository postRepository, ICommunityAccessService communityAccessService)
    {
        _postRepository = postRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task Handle(DislikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);
        
        await _communityAccessService.CheckAccessToPost(request.UserId, post);
        
        var like = await _postRepository.GetLikeAsync(request.UserId, request.PostId);
        if (like == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) doesn't have like on the post ({request.PostId})");

        await _postRepository.DeleteLikeAsync(like);
    }
}