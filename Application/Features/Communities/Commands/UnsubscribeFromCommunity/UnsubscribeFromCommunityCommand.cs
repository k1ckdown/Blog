using MediatR;

namespace Application.Features.Communities.Commands.UnsubscribeFromCommunity;

public sealed record UnsubscribeFromCommunityCommand(Guid UserId, Guid CommunityId) : IRequest;