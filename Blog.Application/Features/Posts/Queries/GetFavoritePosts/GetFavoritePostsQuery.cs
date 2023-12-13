using Blog.Application.DTOs.Posts;
using MediatR;

namespace Blog.Application.Features.Posts.Queries.GetFavoritePosts;

public sealed record GetFavoritePostsQuery(Guid UserId) : IRequest<IEnumerable<PostDto>>;