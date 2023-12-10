using Blog.Application.DTOs.Posts;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetPost;

public sealed record GetPostQuery(Guid UserId, Guid PostId) : IRequest<PostFullDto>;