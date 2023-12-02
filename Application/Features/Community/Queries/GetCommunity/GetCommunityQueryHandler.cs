using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.DTOs.Community;
using AutoMapper;
using MediatR;

namespace Application.Features.Community.Queries.GetCommunity;

public sealed class GetCommunityQueryHandler : IRequestHandler<GetCommunityQuery, CommunityFullDto>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;

    public GetCommunityQueryHandler(IMapper mapper, ICommunityRepository communityRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
    }
    
    public async Task<CommunityFullDto> Handle(GetCommunityQuery request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdAsync(request.CommunityId);
        
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        var communityFullDto = _mapper.Map<CommunityFullDto>(community);
        return communityFullDto;
    }
}