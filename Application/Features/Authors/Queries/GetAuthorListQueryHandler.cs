using Application.Common.Interfaces.Repositories;
using Application.DTOs.Authors;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authors.Queries;

public sealed record GetAuthorListQuery : IRequest<IEnumerable<AuthorDto>>;

public sealed class GetAuthorListQueryHandler : IRequestHandler<GetAuthorListQuery, IEnumerable<AuthorDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetAuthorListQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorListQuery request, CancellationToken cancellationToken) =>
        await _userRepository.Entities
            .Where(user => user.Posts.Count > 0)
            .OrderBy(user => user.FullName)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}