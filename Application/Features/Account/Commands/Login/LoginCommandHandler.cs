using Application.Common.Interfaces.Services;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponse>
{
    private readonly IAccountService _accountService;

    public LoginCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _accountService.LogIn(request.Credentials);
    }
}