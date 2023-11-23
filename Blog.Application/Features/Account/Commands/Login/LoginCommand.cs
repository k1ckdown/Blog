using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Login;

public record struct LoginCommand(LoginCredentials Credentials) : IRequest<TokenResponse>;