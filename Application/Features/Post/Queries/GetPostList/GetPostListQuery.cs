using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Queries.GetPostList;

public sealed record GetPostListQuery(Guid UserId, PostSearchParameters Parameters)
    : IRequest<PostPagedListDto>;