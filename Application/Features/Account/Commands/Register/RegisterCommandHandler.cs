using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Application.DTOs.Account;
using Domain.Entities;
using MediatR;

namespace Application.Features.Account.Commands.Register;

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
        var (tokenResponse, userId) = await _accountService.Register(request.RegisterModel);
        
        var user = new User
        {
            Id = userId,
            CreateTime = DateTime.UtcNow,
            Email = request.RegisterModel.Email,
            Gender = request.RegisterModel.Gender,
            FullName = request.RegisterModel.FullName,
            BirthDate = request.RegisterModel.BirthDate,
            PhoneNumber = request.RegisterModel.PhoneNumber
        };
        await _userRepository.AddAsync(user);
        
        return tokenResponse;
    }
}