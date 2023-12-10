using Blog.Application.Wrappers;
using MediatR;

namespace Blog.Application.Features.Account.Commands.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<TokenResponse>;