using Application.DTOs.Comments;
using MediatR;

namespace Application.Features.Comments.Queries.GetNestedComments;

public sealed record GetNestedCommentsQuery(Guid UserId, Guid CommentId) : IRequest<IEnumerable<CommentDto>>;