using MediatR;

namespace Application.Features.Community.Commands.SubscribeToCommunity;

public sealed record SubscribeToCommunityCommand(Guid UserId, Guid CommunityId) : IRequest;