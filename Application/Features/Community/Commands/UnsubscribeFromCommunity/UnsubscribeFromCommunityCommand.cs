using MediatR;

namespace Application.Features.Community.Commands.UnsubscribeFromCommunity;

public sealed record UnsubscribeFromCommunityCommand(Guid UserId, Guid CommunityId) : IRequest;