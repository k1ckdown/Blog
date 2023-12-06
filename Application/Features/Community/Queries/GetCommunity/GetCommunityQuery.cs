using Application.DTOs.Community;
using MediatR;

namespace Application.Features.Community.Queries.GetCommunity;

public sealed record GetCommunityQuery(Guid CommunityId) : IRequest<CommunityFullDto>;