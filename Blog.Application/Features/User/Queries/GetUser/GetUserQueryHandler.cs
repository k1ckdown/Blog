using AutoMapper;
using Blog.Application.DTOs.Account;
using Blog.Application.Interfaces.Repositories;
using MediatR;

namespace Blog.Application.Features.User.Queries.GetUser;

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
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            throw new Exception();
        }

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}