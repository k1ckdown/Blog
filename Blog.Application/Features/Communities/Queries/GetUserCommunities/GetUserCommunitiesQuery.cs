using Blog.Application.DTOs.Communities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetUserCommunities;

public sealed record GetUserCommunitiesQuery(Guid UserId) : IRequest<IEnumerable<CommunityUserDto>>;