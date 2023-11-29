using MediatR;

namespace Application.Features.Post.Commands.LikePost;

public sealed record LikePostCommand(Guid UserId, Guid PostId) : IRequest;