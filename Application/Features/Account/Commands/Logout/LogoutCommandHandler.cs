using Application.Common.Interfaces.Services;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Response>
{
    private readonly IAccountService _accountService;

    public LogoutCommandHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Response> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _accountService.LogOut();
        return new Response(null, "Logged Out");
    }
}