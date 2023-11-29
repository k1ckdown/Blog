using MediatR;

namespace Application.Features.Post.Commands.DislikePost;

public sealed record DislikePostCommand(Guid UserId, Guid PostId) : IRequest;