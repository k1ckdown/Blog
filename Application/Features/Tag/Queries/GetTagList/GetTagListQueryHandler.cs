using Application.Common.Interfaces.Repositories;
using Application.DTOs.Post;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tag.Queries.GetTagList;

public sealed record GetTagListQuery : IRequest<IEnumerable<TagDto>>;

public sealed class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, IEnumerable<TagDto>>
{
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;

    public GetTagListQueryHandler(IMapper mapper, ITagRepository tagRepository)
    {
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<TagDto>> Handle(GetTagListQuery request, CancellationToken cancellationToken) =>
        await _tagRepository.Entities
            .ProjectTo<TagDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}