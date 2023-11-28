using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Common;
using Application.DTOs.Posts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Posts.Queries.GetPostList;

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
        if (request.Min > request.Max)
            throw new BadRequestException("The minimum reading time cannot exceed the maximum");
        
        var posts = _postRepository.Entities;

        if (request.Author != null)
            posts = posts.Where(post => post.User.FullName.ToLower().Contains(request.Author.ToLower()));

        if (request.Min != null)
            posts = posts.Where(post => post.ReadingTime >= request.Min);

        if (request.Max != null)
            posts = posts.Where(post => post.ReadingTime <= request.Max);

        if (request.Sorting != null)
            posts = request.Sorting switch
            {
                PostSorting.CreateDesc => posts.OrderByDescending(post => post.CreateTime),
                PostSorting.CreateAsc => posts.OrderBy(post => post.CreateTime),
                PostSorting.LikeAsc => posts.OrderBy(post => post.Likes.Count),
                PostSorting.LikeDesc => posts.OrderByDescending(post => post.Likes.Count),
                _ => posts
            };

        if (request.Tags != null) 
            posts = request.Tags.Aggregate(posts, (current, tagId) =>
                current.Where(post => post.Tags.Any(tag => tag.Id == tagId)));

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
}