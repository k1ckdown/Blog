using Application.Common.Interfaces.Services;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Account.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokenResponse>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService) => 
        _authService = authService;

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
        
        var tokenResponse = await _authService.Register(user, request.RegisterModel.Password);
        return tokenResponse;
    }
}