using Application.DTOs.Account;
using MediatR;

namespace Application.Features.Account.Commands.EditUser;

public sealed record EditUserCommand(Guid Id, UserEditModel EditModel) : IRequest;