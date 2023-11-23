using Blog.Application.DTOs.Account;
using MediatR;

namespace Blog.Application.Features.User.Queries.GetUser;

public record struct GetUserQuery(Guid UserId) : IRequest<UserDto>;