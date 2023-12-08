using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Communities.Commands.SubscribeToCommunity;

public sealed class SubscribeToCommunityCommandHandler : IRequestHandler<SubscribeToCommunityCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public SubscribeToCommunityCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task Handle(SubscribeToCommunityCommand request, CancellationToken cancellationToken)
    {
        if (await _communityRepository.Entities.AllAsync(
                community => community.Id != request.CommunityId,
                cancellationToken: cancellationToken))
            throw new NotFoundException(nameof(Community), request.CommunityId);
        
        if (await _communityRepository.Subscriptions.AnyAsync(
                subscription => subscription.UserId == request.UserId && subscription.CommunityId == request.CommunityId,
                cancellationToken: cancellationToken))
            throw new BadRequestException(
                $"The user ({request.UserId}) has already subscribed to the community ({request.CommunityId})");

        var subscription = new Subscription { UserId = request.UserId, CommunityId = request.CommunityId };
        await _communityRepository.AddSubscriptionAsync(subscription);
    }
}