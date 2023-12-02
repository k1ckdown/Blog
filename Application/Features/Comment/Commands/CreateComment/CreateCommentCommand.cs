using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment;

public sealed record CreateCommentCommand(Guid UserId, Guid PostId, CreateCommentDto CreateCommentDto)
    : IRequest;