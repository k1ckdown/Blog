using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Services.Community;
using Domain.Entities;
using MediatR;

namespace Application.Features.Post.Commands.LikePost;

public sealed class LikePostCommandHandler : IRequestHandler<LikePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityService _communityService;

    public LikePostCommandHandler(IPostRepository postRepository, ICommunityService communityService)
    {
        _postRepository = postRepository;
        _communityService = communityService;
    }

    public async Task Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Domain.Entities.Post), request.PostId);

        await _communityService.CheckAccessToPost(request.UserId, post);
        
        if (await _postRepository.GetLikeAsync(request.UserId, request.PostId) != null)
            throw new BadRequestException(
                $"The user ({request.UserId}) already has a like on the post ({request.PostId})");

        var like = new Like { UserId = request.UserId, PostId = request.PostId };
        await _postRepository.AddLikeAsync(like);
    }
}