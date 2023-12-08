using Application.Common.Exceptions;
using Application.Common.Exceptions.Base;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Communities.Commands.UnsubscribeFromCommunity;

public sealed class UnsubscribeFromCommunityCommandHandler : IRequestHandler<UnsubscribeFromCommunityCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public UnsubscribeFromCommunityCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task Handle(UnsubscribeFromCommunityCommand request, CancellationToken cancellationToken)
    {
        if (await _communityRepository.Entities.AllAsync(
                community => community.Id != request.CommunityId,
                cancellationToken: cancellationToken))
            throw new NotFoundException(nameof(Community), request.CommunityId);

        var subscribe = await _communityRepository.GetSubscriptionAsync(request.UserId, request.CommunityId);

        if (subscribe == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) is not subscribed to the community ({request.CommunityId})");

        await _communityRepository.DeleteSubscriptionAsync(subscribe);
    }
}