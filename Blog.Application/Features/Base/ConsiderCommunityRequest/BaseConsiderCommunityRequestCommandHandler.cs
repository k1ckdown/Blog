using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Base.ConsiderCommunityRequest;

public class BaseConsiderCommunityRequestCommandHandler<TRequest> 
    : IRequestHandler<TRequest> where TRequest : BaseConsiderCommunityRequestCommand
{
    protected readonly ICommunityRepository CommunityRepository;

    protected BaseConsiderCommunityRequestCommandHandler(ICommunityRepository communityRepository) =>
        CommunityRepository = communityRepository;
    
    public virtual async Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        var community = await CommunityRepository.GetByIdIncludingRequestsAndAdminsAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        if (community.Administrators?.All(admin => admin.Id != request.UserId) ?? true)
            throw new ForbiddenException(request.UserId);

        var communityRequest = community.Requests?
            .FirstOrDefault(existingRequest => existingRequest.UserId == request.ApplicantId);

        if (communityRequest == null)
            throw new NotFoundException(
                $"The user's ({request.ApplicantId}) request to join the community ({request.CommunityId}) was not found");
        
        community.Requests?.Remove(communityRequest);
        await CommunityRepository.UpdateAsync(community);
    }
}