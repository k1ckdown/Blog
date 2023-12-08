using Application.DTOs.Communities;
using MediatR;

namespace Application.Features.Communities.Queries.GetUserCommunities;

public sealed record GetUserCommunitiesQuery(Guid UserId) : IRequest<IEnumerable<CommunityUserDto>>;