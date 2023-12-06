using MediatR;

namespace Application.Features.Comment.Commands.DeleteComment;

public sealed record DeleteCommentCommand(Guid UserId, Guid CommentId) : IRequest;