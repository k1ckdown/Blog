using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Common;
using Application.DTOs.Post;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Post.Queries.GetPostList;

public sealed class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, PostPagedListDto>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public GetPostListQueryHandler(IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<PostPagedListDto> Handle(GetPostListQuery request, CancellationToken cancellationToken)
    {
        var posts = _postRepository.Entities;
        posts = GetFilteredPosts(posts, request.Parameters);
        posts = GetSortedPosts(posts, request.Parameters.Sorting);

        var totalPages = (int)Math.Ceiling(posts.Count() / (request.Parameters.Size * 1.0));
        if (request.Parameters.Page > totalPages && posts.Any())
            throw new BadRequestException("Invalid value for attribute page");

        var postList = await posts
            .Skip((request.Parameters.Page - 1) * request.Parameters.Size)
            .Take(request.Parameters.Size)
            .Include(post => post.User)
            .Include(post => post.Tags)
            .Include(post => post.Likes)
            .Include(post => post.Comments)
            .Include(post => post.Community)
            .ToListAsync(cancellationToken);
        
        var postListDto = _mapper.Map<List<PostDto>>(postList);
        for (var i = 0; i < postListDto.Count; i++)
            postListDto[i].HasLike = postList[i].Likes.Any(like => like.UserId == request.UserId);
            
        var pageInfo = new PageInfoModel
        {
            Size = Math.Min(request.Parameters.Size, postListDto.Count),
            Count = totalPages,
            Current = request.Parameters.Page
        };

        var pagedList = new PostPagedListDto
        {
            Posts = postListDto,
            Pagination = pageInfo
        };

        return pagedList;
    }

    private static IQueryable<Domain.Entities.Post> GetSortedPosts(
        IQueryable<Domain.Entities.Post> posts,
        PostSorting? sortingType)
    {
        return sortingType switch
        {
            PostSorting.CreateDesc => posts.OrderByDescending(post => post.CreateTime),
            PostSorting.CreateAsc => posts.OrderBy(post => post.CreateTime),
            PostSorting.LikeAsc => posts.OrderBy(post => post.Likes.Count),
            PostSorting.LikeDesc => posts.OrderByDescending(post => post.Likes.Count),
            _ => posts
        };
    }

    private static IQueryable<Domain.Entities.Post> GetFilteredPosts(
        IQueryable<Domain.Entities.Post> posts,
        PostSearchParameters parameters)
    {
        if (parameters.Min > parameters.Max)
            throw new BadRequestException("The minimum reading time cannot exceed the maximum");

        if (parameters.Author != null)
            posts = posts.Where(post => post.User.FullName.ToLower().Contains(parameters.Author.ToLower()));

        if (parameters.Min != null)
            posts = posts.Where(post => post.ReadingTime >= parameters.Min);

        if (parameters.Max != null)
            posts = posts.Where(post => post.ReadingTime <= parameters.Max);

        if (parameters.Tags != null)
            posts = parameters.Tags.Aggregate(posts, (current, tagId) =>
                current.Where(post => post.Tags.Any(tag => tag.Id == tagId)));

        return posts;
    }
}