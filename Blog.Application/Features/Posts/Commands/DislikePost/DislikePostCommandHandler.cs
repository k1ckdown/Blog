using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Communities;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.DislikePost;

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
        var post = await _postRepository.GetByIdIncludingLikes(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);
        
        await _communityAccessService.CheckAccessToPost(request.UserId, post);

        var like = post.Likes.FirstOrDefault(like => like.UserId == request.UserId);
        if (like == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) doesn't have like on the post ({request.PostId})");

        post.Likes.Remove(like);
        await _postRepository.UpdateAsync(post);
    }
}