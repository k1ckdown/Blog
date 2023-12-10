using Blog.Application.DTOs.Posts;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPostList;

public sealed record GetPostListQuery(Guid UserId, PostSearchParameters Parameters)
    : IRequest<PostPagedListDto>;