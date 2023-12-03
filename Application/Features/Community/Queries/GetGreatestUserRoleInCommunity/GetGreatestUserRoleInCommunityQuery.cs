using Application.DTOs.Community;
using MediatR;

namespace Application.Features.Community.Queries.GetGreatestUserRoleInCommunity;

public sealed record GetGreatestUserRoleInCommunityQuery(Guid UserId, Guid CommunityId) 
    : IRequest<CommunityRole?>;