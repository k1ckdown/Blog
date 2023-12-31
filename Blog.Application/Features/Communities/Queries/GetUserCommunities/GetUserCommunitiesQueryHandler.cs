using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Communities;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetUserCommunities;

public sealed class GetUserCommunitiesQueryHandler
    : IRequestHandler<GetUserCommunitiesQuery, IEnumerable<CommunityUserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserCommunitiesQueryHandler(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task<IEnumerable<CommunityUserDto>> Handle(GetUserCommunitiesQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdIncludingCommunitiesAsync(request.UserId);

        if (user == null) throw new NotFoundException(nameof(User), request.UserId);

        var administeredCommunities = user.AdministeredCommunities
            .Select(community => new CommunityUserDto
            {
                UserId = request.UserId,
                CommunityId = community.Id,
                Role = CommunityRole.Administrator
            });

        var subscriptions = user.Subscriptions
            .Where(subscription =>
                administeredCommunities.Any(community => community.CommunityId == subscription.Id) == false)
            .Select(community => new CommunityUserDto
            {
                UserId = request.UserId,
                CommunityId = community.Id,
                Role = CommunityRole.Subscriber
            });

        return administeredCommunities.Concat(subscriptions);
    }
}