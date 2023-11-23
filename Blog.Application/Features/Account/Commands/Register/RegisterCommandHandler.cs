using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Account;
using Blog.Application.Interfaces.Repositories;
using Blog.Domain.Entities;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
{
    private readonly IAccountService _accountService;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IAccountService accountService, IUserRepository userRepository)
    {
        _accountService = accountService;
        _userRepository = userRepository;
    }

    public async Task<TokenResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            CreateTime = DateTime.UtcNow,
            Email = request.RegisterModel.Email,
            Gender = request.RegisterModel.Gender,
            FullName = request.RegisterModel.FullName,
            BirthDate = request.RegisterModel.BirthDate,
            PhoneNumber = request.RegisterModel.PhoneNumber
        };
        
        var tokenResponse = await _accountService.Register(user, request.RegisterModel.Password);
        await _userRepository.AddAsync(user);

        return tokenResponse;
    }
}