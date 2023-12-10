using Blog.Application.DTOs.Communities;
using Blog.Application.DTOs.Posts;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetCommunityPostList;

public sealed record GetCommunityPostListQuery(Guid UserId, Guid CommunityId, CommunityPostSearchParameters Parameters)
    : IRequest<PostPagedListDto>;