using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Communities.Queries.GetGreatestUserRoleInCommunity;

public sealed class GetGreatestUserRoleInCommunityQueryHandler
    : IRequestHandler<GetGreatestUserRoleInCommunityQuery, CommunityRole?>
{
    private readonly ICommunityRepository _communityRepository;

    public GetGreatestUserRoleInCommunityQueryHandler(ICommunityRepository communityRepository) =>
        _communityRepository = communityRepository;

    public async Task<CommunityRole?> Handle(GetGreatestUserRoleInCommunityQuery request, CancellationToken cancellationToken)
    {
        if (await _communityRepository.Entities.AllAsync(
                community => community.Id != request.CommunityId,
                cancellationToken: cancellationToken))
            throw new NotFoundException(nameof(Community), request.CommunityId);

        if (await _communityRepository.Administrators.AnyAsync(
                admin => admin.UserId == request.UserId && admin.CommunityId == request.CommunityId,
                cancellationToken))
            return CommunityRole.Administrator;

        if (await _communityRepository.Subscriptions.AnyAsync(
                subscription => subscription.UserId == request.UserId && subscription.CommunityId == request.CommunityId,
                cancellationToken: cancellationToken))
            return CommunityRole.Subscriber;
        
        return null;
    }
}