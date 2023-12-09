using Application.Common.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Account.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
{
    private readonly IAccountService _accountService;

    public RegisterCommandHandler(IAccountService accountService) => 
        _accountService = accountService;

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
        return tokenResponse;
    }
}