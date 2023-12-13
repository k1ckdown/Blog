using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Posts.Commands.DeleteFavoritePost;

public sealed class DeleteFavoritePostCommandHandler : IRequestHandler<DeleteFavoritePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommunityAccessService _communityAccessService;
    
    public DeleteFavoritePostCommandHandler(IPostRepository postRepository, ICommunityAccessService communityAccessService)
    {
        _postRepository = postRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task Handle(DeleteFavoritePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingFavoriteByUsers(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);

        var favoriteByUser = post.FavoriteByUsers.FirstOrDefault(user => user.Id == request.UserId);
        if (favoriteByUser == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) does not have a favorite post ({request.PostId})");

        post.FavoriteByUsers.Remove(favoriteByUser);
        await _postRepository.UpdateAsync(post);
    }
}