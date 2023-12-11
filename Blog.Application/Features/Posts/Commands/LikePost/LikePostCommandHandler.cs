using Blog.Application.Common.Exceptions;
using Blog.Application.Services.Communities;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.LikePost;

public sealed class LikePostCommandHandler : IRequestHandler<LikePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public LikePostCommandHandler(IPostRepository postRepository, ICommunityAccessService communityAccessService)
    {
        _postRepository = postRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingLikes(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);
        
        if (post.Likes.Any(like => like.UserId == request.UserId))
            throw new BadRequestException(
                $"The user ({request.UserId}) already has a like on the post ({request.PostId})");

        var like = new Like { UserId = request.UserId, PostId = request.PostId };
        post.Likes.Add(like);
        
        await _postRepository.UpdateAsync(post);
    }
}