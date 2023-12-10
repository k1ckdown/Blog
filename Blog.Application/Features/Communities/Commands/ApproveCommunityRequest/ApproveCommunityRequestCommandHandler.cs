using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.Features.Base.CommunityRequest;
using Blog.Domain.Entities;

namespace Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;

public sealed class ApproveCommunityRequestCommandHandler
    : BaseCommunityRequestCommandHandler<ApproveCommunityRequestCommand>
{
    public ApproveCommunityRequestCommandHandler(ICommunityRepository communityRepository) : base(communityRepository)
    {
    }

    public override async Task Handle(ApproveCommunityRequestCommand request, CancellationToken cancellationToken)
    {
        await base.Handle(request, cancellationToken);
        var subscription = new Subscription { UserId = request.ApplicantId, CommunityId = request.CommunityId };
        await CommunityRepository.AddSubscriptionAsync(subscription);
    }
}