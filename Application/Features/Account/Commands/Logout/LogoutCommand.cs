using Application.Wrappers;
using MediatR;

namespace Application.Features.Account.Commands.Logout;

public sealed record LogoutCommand(string Token) : IRequest<Response>;