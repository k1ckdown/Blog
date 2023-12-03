using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Community.Commands.SubscribeToCommunity;

public sealed class SubscribeToCommunityCommandHandler : IRequestHandler<SubscribeToCommunityCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public SubscribeToCommunityCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task Handle(SubscribeToCommunityCommand request, CancellationToken cancellationToken)
    {
        if (await _communityRepository.GetByIdAsync(request.CommunityId) == null)
            throw new NotFoundException(nameof(Domain.Entities.Community), request.CommunityId);
        
        if (await _communityRepository.GetSubscriptionAsync(request.UserId, request.CommunityId) != null)
            throw new BadRequestException(
                $"The user ({request.UserId}) has already subscribed to the community ({request.CommunityId})");

        var subscription = new Subscription { UserId = request.UserId, CommunityId = request.CommunityId };
        await _communityRepository.AddSubscriptionAsync(subscription);
    }
}