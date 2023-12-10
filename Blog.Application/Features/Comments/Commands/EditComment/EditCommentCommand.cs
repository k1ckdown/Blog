using Blog.Application.DTOs.Comments;
using MediatR;

namespace Blog.Application.Features.Comments.Commands.EditComment;

public sealed record EditCommentCommand(Guid UserId, Guid CommentId, UpdateCommentDto UpdateCommentDto) 
    : IRequest;