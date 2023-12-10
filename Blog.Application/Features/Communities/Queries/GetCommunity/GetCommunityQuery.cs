using Blog.Application.DTOs.Communities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetCommunity;

public sealed record GetCommunityQuery(Guid CommunityId) : IRequest<CommunityFullDto>;