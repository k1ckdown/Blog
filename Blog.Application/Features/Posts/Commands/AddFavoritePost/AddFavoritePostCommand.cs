using MediatR;

namespace Blog.Application.Features.Posts.Commands.AddFavoritePost;

public sealed record AddFavoritePostCommand(Guid UserId, Guid PostId) : IRequest;