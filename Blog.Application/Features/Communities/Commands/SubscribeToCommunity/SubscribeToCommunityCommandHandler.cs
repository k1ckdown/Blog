using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Commands.SubscribeToCommunity;

public sealed class SubscribeToCommunityCommandHandler : IRequestHandler<SubscribeToCommunityCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public SubscribeToCommunityCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task Handle(SubscribeToCommunityCommand request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingSubscribersAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);
        
        if (community.Subscribers?.Any(subscriber => subscriber.Id == request.UserId) ?? true)
            throw new BadRequestException(
                $"The user ({request.UserId}) has already subscribed to the community ({request.CommunityId})");

        var subscription = new Subscription { UserId = request.UserId, CommunityId = request.CommunityId };
        await _communityRepository.AddSubscriptionAsync(subscription);
    }
}