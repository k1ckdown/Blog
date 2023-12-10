using MediatR;

namespace Blog.Application.Features.Posts.Commands.LikePost;

public sealed record LikePostCommand(Guid UserId, Guid PostId) : IRequest;