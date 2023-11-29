using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using MediatR;

namespace Application.Features.Post.Commands.DislikePost;

public sealed class DislikePostCommandHandler : IRequestHandler<DislikePostCommand>
{
    private readonly IPostRepository _postRepository;

    public DislikePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Handle(DislikePostCommand request, CancellationToken cancellationToken)
    {
        if (await _postRepository.GetByIdAsync(request.PostId) == null)
            throw new NotFoundException(nameof(Domain.Entities.Post), request.PostId);
        
        var like = await _postRepository.GetLikeAsync(request.UserId, request.PostId);

        if (like == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) doesn't have like on the post ({request.PostId})");

        await _postRepository.DeleteLikeAsync(like);
    }
}