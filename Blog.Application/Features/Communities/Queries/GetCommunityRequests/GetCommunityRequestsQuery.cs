using Blog.Application.DTOs.Communities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetCommunityRequests;

public sealed record GetCommunityRequestsQuery(Guid UserId, Guid CommunityId) 
    : IRequest<IEnumerable<CommunityRequestDto>>;