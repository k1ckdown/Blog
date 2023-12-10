using MediatR;

namespace Blog.Application.Features.Communities.Commands.SubscribeToCommunity;

public sealed record SubscribeToCommunityCommand(Guid UserId, Guid CommunityId) : IRequest;