using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Posts;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Posts.Queries.GetPostList;

public sealed class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, PostPagedListDto>
{
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;

    public GetPostListQueryHandler(IPostService postService, IPostRepository postRepository)
    {
        _postService = postService;
        _postRepository = postRepository;
    }

    public Task<PostPagedListDto> Handle(GetPostListQuery request, CancellationToken cancellationToken)
    {
        var posts = _postRepository.Entities;
        
        posts = GetFilteredPosts(posts, request.Parameters);
        posts = posts
            .Include(post => post.Community)
                .ThenInclude(community => community!.Administrators)
            .Include(post => post.Community)
                .ThenInclude(community => community!.Subscribers)
            .Where(post => post.Community == null 
                           || post.Community.IsClosed == false 
                           || post.Community.Subscribers!.Concat(post.Community.Administrators!)
                               .Any(member => member.Id == request.UserId));
        posts = _postService.Sort(posts, request.Parameters.Sorting);

        return _postService.ToPagedList(posts, request.Parameters.Page, request.Parameters.Size, request.UserId);
    }

    private IQueryable<Post> GetFilteredPosts(IQueryable<Post> posts, PostSearchParameters parameters)
    {
        if (parameters.Min > parameters.Max)
            throw new BadRequestException("The minimum reading time cannot exceed the maximum");

        if (parameters.Author != null)
            posts = _postService.FilterByAuthor(posts, parameters.Author);

        if (parameters.Min != null)
            posts = _postService.FilterByMinReadingTime(posts, parameters.Min ?? 0);

        if (parameters.Max != null)
            posts = _postService.FilterByMaxReadingTime(posts, parameters.Max ?? 0);

        if (parameters.Tags != null)
            posts = _postService.FilterByTags(posts, parameters.Tags);

        return posts;
    }
}