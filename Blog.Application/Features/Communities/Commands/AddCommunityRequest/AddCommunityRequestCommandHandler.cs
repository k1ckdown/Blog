using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Commands.AddCommunityRequest;

public sealed class AddCommunityRequestCommandHandler : IRequestHandler<AddCommunityRequestCommand>
{
    private readonly ICommunityRepository _communityRepository;

    public AddCommunityRequestCommandHandler(ICommunityRepository communityRepository)  => 
        _communityRepository = communityRepository;
    
    public async Task Handle(AddCommunityRequestCommand request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingRequestsAndSubscribersAsync(request.CommunityId); 
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        if (community.IsClosed == false)
            throw new PublicCommunityException(request.CommunityId);
        
        if (community.Subscribers?.Any(subscriber => subscriber.Id == request.UserId) ?? true)
            throw new BadRequestException(
                $"The user ({request.UserId}) has already subscribed to the community ({request.CommunityId})");

        if (community.Requests?.Any(communityRequest => communityRequest.UserId == request.UserId) ?? true)
            throw new BadRequestException(
                $"The user ({request.UserId}) has already added a request to join the community ({request.CommunityId})");

        var communityRequest = new CommunityRequest
        {
            UserId = request.UserId,
            CommunityId = request.CommunityId,
            CreateTime = DateTime.UtcNow,
        };
        
        community.Requests.Add(communityRequest);
        await _communityRepository.UpdateAsync(community);
    }
}