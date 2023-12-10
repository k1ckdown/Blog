using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.Account.Queries.GetUser;

public sealed record GetUserQuery(Guid Id) : IRequest<UserDto>;