using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Login;

public sealed record LoginCommand(LoginCredentials Credentials) : IRequest<TokenResponse>;