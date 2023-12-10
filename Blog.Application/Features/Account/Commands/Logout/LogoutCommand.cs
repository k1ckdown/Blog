using Blog.Application.Wrappers;
using MediatR;

namespace Blog.Application.Features.Account.Commands.Logout;

public sealed record LogoutCommand(string Token) : IRequest<Response>;