using Application.Common.Interfaces.Services;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LogIn(request.Credentials);
    }
}