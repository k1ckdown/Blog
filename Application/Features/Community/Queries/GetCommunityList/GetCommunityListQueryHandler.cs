using Application.Common.Interfaces.Repositories;
using Application.DTOs.Community;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Community.Queries.GetCommunityList;

public sealed record GetCommunityListQuery : IRequest<IEnumerable<CommunityDto>>;

public sealed class GetCommunityListQueryHandler : IRequestHandler<GetCommunityListQuery, IEnumerable<CommunityDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;

    public GetCommunityListQueryHandler(IMapper mapper, ICommunityRepository communityRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
    }

    public async Task<IEnumerable<CommunityDto>> Handle(
        GetCommunityListQuery request,
        CancellationToken cancellationToken) =>
        await _communityRepository.Entities
            .Include(community => community.Subscribers)
            .ProjectTo<CommunityDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}