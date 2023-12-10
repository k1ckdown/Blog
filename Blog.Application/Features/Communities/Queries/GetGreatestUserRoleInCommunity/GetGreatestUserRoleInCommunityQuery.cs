using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetGreatestUserRoleInCommunity;

public sealed record GetGreatestUserRoleInCommunityQuery(Guid UserId, Guid CommunityId) 
    : IRequest<CommunityRole?>;