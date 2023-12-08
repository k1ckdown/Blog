using Application.DTOs.Communities;
using MediatR;

namespace Application.Features.Communities.Queries.GetCommunity;

public sealed record GetCommunityQuery(Guid CommunityId) : IRequest<CommunityFullDto>;