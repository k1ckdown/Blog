using Application.DTOs.Post;
using MediatR;

namespace Application.Features.Post.Commands;

public sealed record CreatePostCommand(Guid UserId, CreatePostDto CreatePostDto) : IRequest<Guid>;