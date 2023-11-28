using Application.DTOs.Posts;
using MediatR;

namespace Application.Features.Posts.Commands;

public sealed record CreatePostCommand(Guid UserId, CreatePostDto CreatePostDto) : IRequest<Guid>;