using Blog.Application.DTOs.Comments;
using MediatR;

namespace Blog.Application.Features.Comments.Queries.GetNestedComments;

public sealed record GetNestedCommentsQuery(Guid UserId, Guid CommentId) : IRequest<IEnumerable<CommentDto>>;