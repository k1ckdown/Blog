using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comment.Commands.EditComment;

public sealed record EditCommentCommand(Guid UserId, Guid CommentId, UpdateCommentDto UpdateCommentDto) 
    : IRequest;