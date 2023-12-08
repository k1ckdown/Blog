using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Posts;
using Domain.Entities;
using MediatR;

namespace Application.Features.Communities.Queries.GetCommunityPostList;

public sealed class GetCommunityPostListQueryHandler : IRequestHandler<GetCommunityPostListQuery, PostPagedListDto>
{
    private readonly IPostService _postService;
    private readonly ICommunityAccessService _communityAccessService;
    
    private readonly IPostRepository _postRepository;
    private readonly ICommunityRepository _communityRepository;

    public GetCommunityPostListQueryHandler(
        IPostService postService,
        ICommunityAccessService communityAccessService,
        IPostRepository postRepository,
        ICommunityRepository communityRepository)
    {
        _postService = postService;
        _postRepository = postRepository;
        _communityRepository = communityRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task<PostPagedListDto> Handle(GetCommunityPostListQuery request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);
        
        await _communityAccessService.CheckAccess(request.UserId, community);

        var posts = _postRepository.Entities
            .Where(post => post.CommunityId == request.CommunityId);

        if (request.Parameters.Tags != null) posts = _postService.FilterByTags(posts, request.Parameters.Tags);
        posts = _postService.Sort(posts, request.Parameters.Sorting);

        return await _postService.ToPagedList(posts, request.Parameters.Page, request.Parameters.Size, request.UserId);
    }
}