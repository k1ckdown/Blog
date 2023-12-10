using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.RefreshToken;

public sealed record RefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : IRequest<TokenResponse>;