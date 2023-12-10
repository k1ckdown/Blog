using AutoMapper;
using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Communities;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Communities.Queries.GetCommunityRequests;

public sealed class GetCommunityRequestsQueryHandler 
    : IRequestHandler<GetCommunityRequestsQuery, IEnumerable<CommunityRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly ICommunityRepository _communityRepository;

    public GetCommunityRequestsQueryHandler(IMapper mapper, ICommunityRepository communityRepository)
    {
        _mapper = mapper;
        _communityRepository = communityRepository;
    }

    public async Task<IEnumerable<CommunityRequestDto>> Handle(GetCommunityRequestsQuery request, CancellationToken cancellationToken)
    {
        var community = await _communityRepository.GetByIdIncludingRequestsAndAdminsAsync(request.CommunityId);
        if (community == null) throw new NotFoundException(nameof(Community), request.CommunityId);
        
        if (community.IsClosed == false)
            throw new PublicCommunityException(request.CommunityId);

        if (community.Administrators?.All(admin => admin.Id != request.UserId) ?? true)
            throw new ForbiddenException(
                $"Access is forbidden for user ({request.UserId}). Community requests are available only to administrators");

        var communityRequests = community.Requests?
            .Select(communityRequest => _mapper.Map<CommunityRequestDto>(communityRequest));

        return communityRequests ?? Array.Empty<CommunityRequestDto>();
    }
}