using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comment.Queries.GetNestedComments;

public sealed record GetNestedCommentsQuery(Guid CommentId) : IRequest<IEnumerable<CommentDto>>;