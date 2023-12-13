using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.AddFavoritePost;

public sealed class AddFavoritePostCommandHandler : IRequestHandler<AddFavoritePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityAccessService _communityAccessService;


    public AddFavoritePostCommandHandler(IPostRepository postRepository, ICommunityAccessService communityAccessService)
    {
        _postRepository = postRepository;
        _communityAccessService = communityAccessService;
    }
    
    public async Task Handle(AddFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingFavoriteByUsers(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);

        if (post.FavoriteByUsers.Any(user => user.Id == request.UserId))
            throw new BadRequestException(
                $"The user ({request.UserId}) has already added the post ({request.PostId}) to favorites");

        var favoritePost = new FavoritePost { UserId = request.UserId, PostId = request.PostId };
        await _postRepository.AddFavoritePostAsync(favoritePost);
    }
}