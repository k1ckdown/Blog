using Application.DTOs.Account;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Register;

public sealed record RegisterCommand(UserRegisterModel RegisterModel) : IRequest<TokenResponse>;