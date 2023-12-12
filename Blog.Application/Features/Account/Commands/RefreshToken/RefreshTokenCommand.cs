using Blog.Application.DTOs.Tokens;
using MediatR;

namespace Blog.Application.Features.Account.Commands.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<TokenResponse>;