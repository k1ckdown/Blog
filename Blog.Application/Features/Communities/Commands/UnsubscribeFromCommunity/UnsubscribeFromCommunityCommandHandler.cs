using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Communities.Commands.UnsubscribeFromCommunity;

public sealed class UnsubscribeFromCommunityCommandHandler : IRequestHandler<UnsubscribeFromCommunityCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public UnsubscribeFromCommunityCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task Handle(UnsubscribeFromCommunityCommand request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingSubscribersAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        var subscriber = community.Subscribers?.FirstOrDefault(subscriber => subscriber.Id == request.UserId);
        if (subscriber == null)
            throw new NotFoundException(
                $"The user ({request.UserId}) is not subscribed to the community ({request.CommunityId})");

        community.Subscribers?.Remove(subscriber);
        await _communityRepository.UpdateAsync(community);
    }
}