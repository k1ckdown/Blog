using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Common;
using Application.DTOs.Post;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        posts = GetFilteredPosts(posts, request.Author, request.Min, request.Max, request.Tags);
        posts = GetSortedPosts(posts, request.Sorting);

        var postList = await posts
            .Skip((request.Page - 1) * request.Size)
            .Take(request.Size)
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var count = (int)Math.Ceiling(posts.Count() / (request.Size * 1.0));
        if (request.Page > count && posts.Any()) throw new BadRequestException("Invalid value for attribute page");

        var pageInfo = new PageInfoModel
        {
            Size = Math.Min(request.Size, postList.Count),
            Count = count,
            Current = request.Page
        };

        var pagedList = new PostPagedListDto
        {
            Posts = postList,
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
        string? authorName,
        int? minReadingTime,
        int? maxReadingTime,
        IEnumerable<Guid>? tagIds)
    {
        if (minReadingTime > maxReadingTime)
            throw new BadRequestException("The minimum reading time cannot exceed the maximum");

        if (authorName != null)
            posts = posts.Where(post => post.User.FullName.ToLower().Contains(authorName.ToLower()));

        if (minReadingTime != null)
            posts = posts.Where(post => post.ReadingTime >= minReadingTime);

        if (maxReadingTime != null)
            posts = posts.Where(post => post.ReadingTime <= maxReadingTime);

        if (tagIds != null)
            posts = tagIds.Aggregate(posts, (current, tagId) =>
                current.Where(post => post.Tags.Any(tag => tag.Id == tagId)));

        return posts;
    }
}