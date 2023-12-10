using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Authors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Authors.Queries;

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