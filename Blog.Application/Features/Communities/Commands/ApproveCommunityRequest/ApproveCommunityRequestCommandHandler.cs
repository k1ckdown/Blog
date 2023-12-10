using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Commands.ApproveCommunityRequest;

public sealed class ApproveCommunityRequestCommandHandler : IRequestHandler<ApproveCommunityRequestCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public ApproveCommunityRequestCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;
    
    public async Task Handle(ApproveCommunityRequestCommand request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingRequestsAndAdminsAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        if (community.Administrators?.All(admin => admin.Id != request.UserId) ?? true)
            throw new ForbiddenException(request.UserId);

        var communityRequest = community.Requests?
            .FirstOrDefault(existingRequest => existingRequest.UserId == request.ApplicantId);

        if (communityRequest == null)
            throw new NotFoundException(
                $"The user's ({request.ApplicantId}) request to join the community ({request.CommunityId}) was not found");
        
        community.Requests?.Remove(communityRequest);
        await _communityRepository.UpdateAsync(community);
        
        var subscription = new Subscription { UserId = request.ApplicantId, CommunityId = request.CommunityId };
        await _communityRepository.AddSubscriptionAsync(subscription);
    }
}