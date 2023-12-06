using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;

namespace Application.Features.Comment;

public abstract class BaseCommentRequestHandler
{
    protected readonly IPostRepository PostRepository;

    protected BaseCommentRequestHandler(IPostRepository postRepository) =>
        PostRepository = postRepository;

    protected static void CheckAccess(Guid userId, Domain.Entities.Post post)
    {
        if (post.Community != null && post.Community.Subscribers.All(subscriber => subscriber.Id != userId))
            throw new ForbiddenException(
                $"Access to closed community post ({post.Id}) is forbidden for user ({userId})");
    }
    
    protected async Task CheckAccess(Guid userId, Guid postId)
    {
        var post = await PostRepository.GetByIdAsync(postId);
        if (post == null) throw new NotFoundException(nameof(Post), postId);
        CheckAccess(userId, post);
    }
}