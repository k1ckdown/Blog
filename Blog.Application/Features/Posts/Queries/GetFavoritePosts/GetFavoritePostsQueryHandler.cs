using AutoMapper;
using Blog.Application.Common.Extensions;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Posts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Posts.Queries.GetFavoritePosts;

public sealed class GetFavoritePostsQueryHandler : IRequestHandler<GetFavoritePostsQuery, IEnumerable<PostDto>>
{
    private readonly IMapper _mapper;
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;

    public GetFavoritePostsQueryHandler(
        IMapper mapper, 
        IPostService postService, 
        IPostRepository postRepository)
    {
        _mapper = mapper;
        _postService = postService;
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostDto>> Handle(GetFavoritePostsQuery request, CancellationToken cancellationToken)
    {
        var postList = await _postRepository.Entities
            .IncludeAll()
            .Where(post => post.FavoriteByUsers.Any(user => user.Id == request.UserId))
            .ToListAsync(cancellationToken);

        return _postService.ToListDto(request.UserId, postList);
    }
}