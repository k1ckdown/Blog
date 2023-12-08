using Application.DTOs.Comments;
using MediatR;

namespace Application.Features.Comments.Commands.EditComment;

public sealed record EditCommentCommand(Guid UserId, Guid CommentId, UpdateCommentDto UpdateCommentDto) 
    : IRequest;