using Application.DTOs.Comments;
using MediatR;

namespace Application.Features.Comments.Commands.CreateComment;

public sealed record CreateCommentCommand(Guid UserId, Guid PostId, CreateCommentDto CreateCommentDto)
    : IRequest;