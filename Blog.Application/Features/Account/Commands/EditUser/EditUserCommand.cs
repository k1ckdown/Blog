using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.Account.Commands.EditUser;

public sealed record EditUserCommand(Guid Id, UserEditModel EditModel) : IRequest;