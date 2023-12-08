using Application.DTOs.Communities;
using Application.DTOs.Posts;
using MediatR;

namespace Application.Features.Communities.Queries.GetCommunityPostList;

public sealed record GetCommunityPostListQuery(Guid UserId, Guid CommunityId, CommunityPostSearchParameters Parameters)
    : IRequest<PostPagedListDto>;