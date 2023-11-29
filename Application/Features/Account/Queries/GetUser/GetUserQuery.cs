using Application.DTOs.Account;
using MediatR;

namespace Application.Features.Account.Queries.GetUser;

public sealed record GetUserQuery(Guid Id) : IRequest<UserDto>;