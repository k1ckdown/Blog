using MediatR;

namespace Application.Features.Posts.Commands.LikePost;

public sealed record LikePostCommand(Guid UserId, Guid PostId) : IRequest;