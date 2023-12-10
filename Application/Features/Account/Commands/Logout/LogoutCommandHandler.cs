using Application.Common.Interfaces.Services;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Response>
{
    private readonly IAuthService _authService;

    public LogoutCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<Response> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _authService.LogOut(request.Token);
        return new Response(null, "Logged Out");
    }
}