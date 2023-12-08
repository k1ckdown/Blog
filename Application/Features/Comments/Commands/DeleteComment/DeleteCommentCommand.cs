using MediatR;

namespace Application.Features.Comments.Commands.DeleteComment;

public sealed record DeleteCommentCommand(Guid UserId, Guid CommentId) : IRequest;