using MediatR;

namespace Blog.Application.Features.Posts.Commands.DislikePost;

public sealed record DislikePostCommand(Guid UserId, Guid PostId) : IRequest;