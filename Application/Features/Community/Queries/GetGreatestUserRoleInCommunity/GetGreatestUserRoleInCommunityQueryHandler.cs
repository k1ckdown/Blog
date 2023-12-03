using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Community.Queries.GetGreatestUserRoleInCommunity;

public sealed class GetGreatestUserRoleInCommunityQueryHandler
    : IRequestHandler<GetGreatestUserRoleInCommunityQuery, CommunityRole?>
{
    private readonly ICommunityRepository _communityRepository;

    public GetGreatestUserRoleInCommunityQueryHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task<CommunityRole?> Handle(GetGreatestUserRoleInCommunityQuery request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdAsync(request.CommunityId);

        if (community == null)
            throw new NotFoundException(nameof(Community), request.CommunityId);

        if (community.Administrators.Any(administrator => administrator.Id == request.UserId))
            return CommunityRole.Administrator;

        if (community.Subscribers.Any(subscriber => subscriber.Id == request.UserId))
            return CommunityRole.Subscriber;

        return null;
    }
}