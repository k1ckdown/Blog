using Blog.Application.DTOs.Account;
using Blog.Application.DTOs.Tokens;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Register;

public sealed record RegisterCommand(UserRegisterModel RegisterModel) : IRequest<TokenResponse>;