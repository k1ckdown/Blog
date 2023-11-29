using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Post.Commands.LikePost;

public sealed class LikePostCommandHandler : IRequestHandler<LikePostCommand>
{
    private readonly IPostRepository _postRepository;

    public LikePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        if (await _postRepository.GetByIdAsync(request.PostId) == null)
            throw new NotFoundException(nameof(Domain.Entities.Post), request.PostId);

        if (await _postRepository.GetLikeAsync(request.UserId, request.PostId) != null)
            throw new BadRequestException(
                $"The user ({request.UserId}) already has a like on the post ({request.PostId})");

        var like = new Like { UserId = request.UserId, PostId = request.PostId };
        await _postRepository.AddLikeAsync(like);
    }
}