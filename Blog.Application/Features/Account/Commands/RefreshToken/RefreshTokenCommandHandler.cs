using Blog.Application.Common.Interfaces.Services;
using Blog.Application.DTOs.Tokens;
using MediatR;

namespace Blog.Application.Features.Account.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService) =>
        _authService = authService;

    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authService.Refresh(request.RefreshTokenRequest.Token);
    }
}