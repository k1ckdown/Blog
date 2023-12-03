using Application.DTOs.Community;
using MediatR;

namespace Application.Features.Community.Queries.GetUserCommunities;

public sealed record GetUserCommunitiesQuery(Guid UserId) : IRequest<IEnumerable<CommunityUserDto>>;