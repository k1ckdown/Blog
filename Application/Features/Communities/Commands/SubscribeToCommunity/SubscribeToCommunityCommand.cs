using MediatR;

namespace Application.Features.Communities.Commands.SubscribeToCommunity;

public sealed record SubscribeToCommunityCommand(Guid UserId, Guid CommunityId) : IRequest;