using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Services.Community;
using MediatR;

namespace Application.Features.Post.Commands.DislikePost;

public sealed class DislikePostCommandHandler : IRequestHandler<DislikePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityService _communityService;

    public DislikePostCommandHandler(IPostRepository postRepository, ICommunityService communityService)
    {
        _postRepository = postRepository;
        _communityService = communityService;
    }

    public async Task Handle(DislikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Domain.Entities.Post), request.PostId);
        
        await _communityService.CheckAccessToPost(request.UserId, post);
        
        var like = await _postRepository.GetLikeAsync(request.UserId, request.PostId);
        if (like == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) doesn't have like on the post ({request.PostId})");

        await _postRepository.DeleteLikeAsync(like);
    }
}