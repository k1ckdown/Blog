using Domain.Entities;
using MediatR;

namespace Application.Features.Communities.Queries.GetGreatestUserRoleInCommunity;

public sealed record GetGreatestUserRoleInCommunityQuery(Guid UserId, Guid CommunityId) 
    : IRequest<CommunityRole?>;