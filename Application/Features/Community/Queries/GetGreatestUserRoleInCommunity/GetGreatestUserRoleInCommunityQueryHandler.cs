using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Community;
using Application.Services.Community;
using MediatR;

namespace Application.Features.Community.Queries.GetGreatestUserRoleInCommunity;

public sealed class GetGreatestUserRoleInCommunityQueryHandler 
    : IRequestHandler<GetGreatestUserRoleInCommunityQuery, CommunityRole?>
{
    private readonly ICommunityService _communityService;
    private readonly ICommunityRepository _communityRepository;

    public GetGreatestUserRoleInCommunityQueryHandler(
        ICommunityService communityService, 
        ICommunityRepository communityRepository)
    {
        _communityService = communityService;
        _communityRepository = communityRepository;
    }
    
    public async Task<CommunityRole?> Handle(GetGreatestUserRoleInCommunityQuery request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdAsync(request.CommunityId);

        if (community == null)
            throw new NotFoundException(nameof(Community), request.CommunityId);

        return _communityService.GetGreatestUserRole(request.UserId, community);
    }
}