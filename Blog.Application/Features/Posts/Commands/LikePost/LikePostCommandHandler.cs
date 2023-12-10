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
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);
        
        if (await _postRepository.GetLikeAsync(request.UserId, request.PostId) != null)
            throw new BadRequestException(
                $"The user ({request.UserId}) already has a like on the post ({request.PostId})");

        var like = new Like { UserId = request.UserId, PostId = request.PostId };
        await _postRepository.AddLikeAsync(like);
    }
}