using Application.DTOs.Account;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Login;

public sealed record LoginCommand(LoginCredentials Credentials) : IRequest<TokenResponse>;