using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Commands.DeleteCommunityRequest;

public sealed class DeleteCommunityRequestCommandHandler : IRequestHandler<DeleteCommunityRequestCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public DeleteCommunityRequestCommandHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;
    
    public async Task Handle(DeleteCommunityRequestCommand request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingRequests(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        var communityRequest = community.Requests?
            .FirstOrDefault(existingRequest => existingRequest.UserId == request.UserId);
        
        if (communityRequest == null)
            throw new CommunityRequestNotFoundException(request.UserId, request.CommunityId);

        if (communityRequest.UserId != request.UserId)
            throw new ForbiddenException(
                $"The user ({request.UserId}) is not the owner of the request to join this community ({request.CommunityId}).");
        
        community.Requests?.Remove(communityRequest);
        await _communityRepository.UpdateAsync(community);
    }
}