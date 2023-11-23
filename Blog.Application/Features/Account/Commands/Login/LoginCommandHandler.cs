using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Login;

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