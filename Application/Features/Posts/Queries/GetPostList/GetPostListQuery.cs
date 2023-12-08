using Application.DTOs.Posts;
using MediatR;

namespace Application.Features.Posts.Queries.GetPostList;

public sealed record GetPostListQuery(Guid UserId, PostSearchParameters Parameters)
    : IRequest<PostPagedListDto>;