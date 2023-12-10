using Blog.Application.Common.Exceptions;
using AutoMapper;
using Blog.Application.Common.Exceptions.Base;
using Blog.Application.Common.Interfaces.Repositories;
using Blog.Application.DTOs.Account;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Account.Queries.GetUser;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null) throw new NotFoundException(nameof(User), request.Id);
        
        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}