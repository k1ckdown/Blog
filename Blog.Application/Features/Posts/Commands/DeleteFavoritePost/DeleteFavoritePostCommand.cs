using MediatR;

namespace Blog.Application.Features.Posts.Commands.DeleteFavoritePost;

public sealed record DeleteFavoritePostCommand(Guid UserId, Guid PostId) : IRequest;