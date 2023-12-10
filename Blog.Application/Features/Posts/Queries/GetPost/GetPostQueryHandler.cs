using AutoMapper;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Posts;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPost;

public sealed class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostFullDto>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ICommunityAccessService _communityAccessService;

    public GetPostQueryHandler(
        IMapper mapper, 
        IPostRepository postRepository,
        ICommunityAccessService communityAccessService)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _communityAccessService = communityAccessService;
    }

    public async Task<PostFullDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdIncludingAllAsync(request.PostId);
        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        await _communityAccessService.CheckAccessToPost(request.UserId, post);
        
        var postFullDto = _mapper.Map<PostFullDto>(post);
        postFullDto.HasLike = post.Likes.Any(like => like.UserId == request.UserId);

        return postFullDto;
    }
}