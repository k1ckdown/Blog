using Blog.Application.Common.Exceptions;
using AutoMapper;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Communities;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetCommunity;

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
        var community = await _communityRepository.GetByIdIncludingAllMembersAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);

        var communityFullDto = _mapper.Map<CommunityFullDto>(community);
        return communityFullDto;
    }
}