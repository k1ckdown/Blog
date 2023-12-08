using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Common;
using Application.DTOs.Posts;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public sealed class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;

    public PostService(IMapper mapper, ITagRepository tagRepository)
    {
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public IQueryable<Post> FilterByAuthor(IQueryable<Post> posts, string author)
    {
        return posts.Where(post => post.User.FullName.ToLower().Contains(author.ToLower()));
    }

    public IQueryable<Post> FilterByMinReadingTime(IQueryable<Post> posts, int minTime) =>
        posts.Where(post => post.ReadingTime >= minTime);
    
    public IQueryable<Post> FilterByMaxReadingTime(IQueryable<Post> posts, int maxTime) =>
        posts.Where(post => post.ReadingTime <= maxTime);

    public IQueryable<Post> FilterByTags(IQueryable<Post> posts, IList<Guid> tagIdentifiers)
    {
        var existingTagIds = new HashSet<Guid>(_tagRepository.Entities.Select(tag => tag.Id));

        foreach (var id in tagIdentifiers)
        {
            if (existingTagIds.Contains(id) == false)
                throw new NotFoundException(nameof(Tag), id);
        }

        return tagIdentifiers.Aggregate(posts, (current, tagId) => 
            current.Where(post => post.Tags.Any(tag => tag.Id == tagId)));
    }

    public IQueryable<Post> Sort(IQueryable<Post> posts, PostSorting? sortingType) =>
        sortingType switch
        {
            PostSorting.CreateDesc => posts.OrderByDescending(post => post.CreateTime),
            PostSorting.CreateAsc => posts.OrderBy(post => post.CreateTime),
            PostSorting.LikeAsc => posts.OrderBy(post => post.Likes.Count),
            PostSorting.LikeDesc => posts.OrderByDescending(post => post.Likes.Count),
            _ => posts
        };
    
    public async Task<PostPagedListDto> ToPagedList(IQueryable<Post> posts, int page, int size, Guid userId)
    {
        var (pagedPosts, totalPages) = posts.ToPaged(page, size);
        var pagedList = await pagedPosts
            .IncludeAll()
            .ToListAsync();

        var postListDto = _mapper.Map<List<PostDto>>(pagedList);
        for (var i = 0; i < postListDto.Count; i++)
            postListDto[i].HasLike = pagedList[i].Likes.Any(like => like.UserId == userId);

        var pageInfo = new PageInfoModel
        {
            Size = Math.Min(size, postListDto.Count),
            Count = totalPages,
            Current = page
        };

        return new PostPagedListDto { Posts = postListDto, Pagination = pageInfo };
    }
}